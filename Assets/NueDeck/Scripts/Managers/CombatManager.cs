using System;
using System.Collections;
using System.Collections.Generic;
using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Characters.Enemies;
using NueDeck.Scripts.Collection;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.UI;
using UnityEngine;

namespace NueDeck.Scripts.Managers
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager instance;
        
        public List<Transform> enemyPosList;
        public List<Transform> allyPosList;
        
        [HideInInspector] public List<EnemyBase> currentEnemies = new List<EnemyBase>();
        [HideInInspector] public List<AllyBase> currentAllies = new List<AllyBase>();

        public Action OnAllyTurnStarted;
        public Action OnEnemyTurnStarted;
        
        #region Setup
        
        public CombatState CurrentCombatState
        {
            get => _currentCombatState;
            set
            {
                ExecuteCombatState(value);
                _currentCombatState = value;
            }
        }
        private CombatState _currentCombatState;

        private void Awake()
        {
            instance = this;
            
            CurrentCombatState = CombatState.PrepareCombat;
        }

        private void Start()
        {
            StartCombat();
        }

        public void StartCombat()
        {
            BuildEnemies();
            BuildAllies();
          
            CollectionManager.instance.SetGameDeck();
            CollectionManager.instance.rewardController.choiceParent.gameObject.SetActive(false);
            UIManager.instance.combatCanvas.gameObject.SetActive(true);
            UIManager.instance.informationCanvas.gameObject.SetActive(true);
            CurrentCombatState = CombatState.AllyTurn;
        }
        
        private void ExecuteCombatState(CombatState targetState)
        {
            switch (targetState)
            {
                case CombatState.PrepareCombat:
                    break;
                case CombatState.AllyTurn:

                    OnAllyTurnStarted?.Invoke();
                    
                    GameManager.instance.PersistentGameplayData.CurrentMana = GameManager.instance.PersistentGameplayData.MAXMana;
                    
                    CollectionManager.instance.DrawCards(GameManager.instance.PersistentGameplayData.DrawCount);
                    
                    GameManager.instance.PersistentGameplayData.CanSelectCards = true;
                    
                    break;
                case CombatState.EnemyTurn:

                    OnEnemyTurnStarted?.Invoke();
                    
                    CollectionManager.instance.DiscardHand();
                    
                    StartCoroutine(nameof(EnemyTurnRoutine));
                    
                    GameManager.instance.PersistentGameplayData.CanSelectCards = false;
                    
                    break;
                case CombatState.EndCombat:
                    
                    GameManager.instance.PersistentGameplayData.CanSelectCards = false;
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetState), targetState, null);
            }
        }

        #endregion
        
        #region Methods

        public void EndTurn()
        {
            CurrentCombatState = CombatState.EnemyTurn;
        }
        
        public void OnAllyDeath(AllyBase targetAlly)
        {
            currentAllies.Remove(targetAlly);
            Debug.Log(currentAllies.Count);
            if (currentAllies.Count<=0)
                LoseCombat();
        } 
        
        public void OnEnemyDeath(EnemyBase targetEnemy)
        {
            currentEnemies.Remove(targetEnemy);
            if (currentEnemies.Count<=0)
                WinCombat();
        }
        
        private void BuildEnemies()
        {
            var encounter = GameManager.instance.EncounterData.GetEnemyEncounter(GameManager.instance.PersistentGameplayData.CurrentStageId,GameManager.instance.PersistentGameplayData.CurrentEncounterId,GameManager.instance.PersistentGameplayData.IsFinalEncounter).enemyList;
            for (var i = 0; i < encounter.Count; i++)
            {
                var clone = Instantiate(encounter[i], enemyPosList.Count >= i ? enemyPosList[i] : enemyPosList[0]);
                clone.BuildCharacter();
                currentEnemies.Add(clone);
            }
        }
        
        private void BuildAllies()
        {
            for (var i = 0; i < GameManager.instance.PersistentGameplayData.AllyList.Count; i++)
            {
                var clone = Instantiate(GameManager.instance.PersistentGameplayData.AllyList[i], allyPosList.Count >= i ? allyPosList[i] : allyPosList[0]);
                clone.BuildCharacter();
                currentAllies.Add(clone);
            }
        }
        
        public void DeactivateCardHighlights()
        {
            foreach (var currentEnemy in currentEnemies)
                currentEnemy.enemyCanvas.SetHighlight(false);

            foreach (var currentAlly in currentAllies)
                currentAlly.allyCanvas.SetHighlight(false);
        }

        public void IncreaseMana(int target)
        {
            GameManager.instance.PersistentGameplayData.CurrentMana += target;
            UIManager.instance.combatCanvas.SetPileTexts();
        }
        
        public void HighlightCardTarget(ActionTargets targetTargets)
        {
            switch (targetTargets)
            {
                case ActionTargets.Enemy:
                    foreach (var currentEnemy in CombatManager.instance.currentEnemies)
                        currentEnemy.enemyCanvas.SetHighlight(true);
                    break;
                case ActionTargets.Ally:
                    foreach (var currentAlly in CombatManager.instance.currentAllies)
                       currentAlly.allyCanvas.SetHighlight(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetTargets), targetTargets, null);
            }
        }
        
        public void LoseCombat()
        {
            CurrentCombatState = CombatState.EndCombat;
            CollectionManager.instance.DiscardHand();
            CollectionManager.instance.discardPile.Clear();
            CollectionManager.instance.drawPile.Clear();
            CollectionManager.instance.handPile.Clear();
            CollectionManager.instance.handController.hand.Clear();
            UIManager.instance.combatCanvas.gameObject.SetActive(true);
            UIManager.instance.combatCanvas.combatLosePanel.SetActive(true);
        }

        public void WinCombat()
        {
            CurrentCombatState = CombatState.EndCombat;

            foreach (var currentAlly in currentAllies)
            {
                GameManager.instance.PersistentGameplayData.CurrentHealthDict[currentAlly.allyData.characterID] = currentAlly.CharacterStats.CurrentHealth;
                GameManager.instance.PersistentGameplayData.MaxHealthDict[currentAlly.allyData.characterID] = currentAlly.CharacterStats.MaxHealth;
            }
            
            CollectionManager.instance.discardPile.Clear();
            CollectionManager.instance.drawPile.Clear();
            CollectionManager.instance.handPile.Clear();
            CollectionManager.instance.handController.hand.Clear();
            
           
            if (GameManager.instance.PersistentGameplayData.IsFinalEncounter)
            {
                UIManager.instance.combatCanvas.combatWinPanel.SetActive(true);
            }
            else
            {
                GameManager.instance.PersistentGameplayData.CurrentEncounterId++;
                UIManager.instance.combatCanvas.gameObject.SetActive(false);
                UIManager.instance.rewardCanvas.gameObject.SetActive(true);
                UIManager.instance.rewardCanvas.BuildReward(RewardType.Gold);
                UIManager.instance.rewardCanvas.BuildReward(RewardType.Card);
            }
           
        }
    
        #endregion
        
        #region Routines

        private IEnumerator EnemyTurnRoutine()
        {
            var waitDelay = new WaitForSeconds(0.1f);

            foreach (var currentEnemy in currentEnemies)
            {
                yield return currentEnemy.StartCoroutine(nameof(EnemyExample.ActionRoutine));
                yield return waitDelay;
            }

            if (CurrentCombatState != CombatState.EndCombat)
            {
                CurrentCombatState = CombatState.AllyTurn;
            }
        }
        

        #endregion

    }
}