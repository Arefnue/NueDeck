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
        public CombatUI combatUI;
        
        
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
        
        public void StartCombat()
        {
            BuildEnemies();
            BuildAllies();
            
            CollectionManager.instance.SetGameDeck();
            CollectionManager.instance.rewardController.choiceParent.gameObject.SetActive(false);
            combatUI.gameCanvas.SetActive(true);
            
            CurrentCombatState = CombatState.AllyTurn;
        }
        
        private void ExecuteCombatState(CombatState targetState)
        {
            switch (targetState)
            {
                case CombatState.PrepareCombat:
                    break;
                case CombatState.AllyTurn:
                    
                    GameManager.instance.PersistentGameplayData.CurrentMana = GameManager.instance.PersistentGameplayData.MAXMana;
                    CollectionManager.instance.DrawCards(GameManager.instance.PersistentGameplayData.DrawCount);
                    foreach (var currentEnemy in currentEnemies) currentEnemy.ShowNextAbility();
                    GameManager.instance.PersistentGameplayData.CanSelectCards = true;
                    
                    break;
                case CombatState.EnemyTurn:
                    
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
            if (currentAllies.Count<=0)
                LevelManager.instance.LoseGame();
        }
        
        private void BuildEnemies()
        {
            for (var i = 0; i < LevelManager.instance.levelEnemyList.Count; i++)
            {
                var clone = Instantiate(LevelManager.instance.levelEnemyList[i], enemyPosList.Count >= i ? enemyPosList[i] : enemyPosList[0]);
                currentEnemies.Add(clone);
            }
        }
        
        private void BuildAllies()
        {
            for (var i = 0; i < GameManager.instance.PersistentGameplayData.AllyList.Count; i++)
            {
                var clone = Instantiate(GameManager.instance.PersistentGameplayData.AllyList[i], allyPosList.Count >= i ? allyPosList[i] : allyPosList[0]);
                currentAllies.Add(clone);
            }
        }
        
        public void DeactivateCardHighlights()
        {
            foreach (var currentEnemy in currentEnemies)
                currentEnemy.highlightObject.SetActive(false);

            foreach (var currentAlly in currentAllies)
                currentAlly.highlightObject.SetActive(false);
        }

        public void IncreaseMana(int target)
        {
            GameManager.instance.PersistentGameplayData.CurrentMana += target;
            combatUI.SetPileTexts();
        }
        
        public void HighlightCardTarget(ActionTargets targetTargets)
        {
            switch (targetTargets)
            {
                case ActionTargets.Enemy:
                    foreach (var currentEnemy in CombatManager.instance.currentEnemies)
                        currentEnemy.highlightObject.SetActive(true);
                    break;
                case ActionTargets.Ally:
                    foreach (var currentAlly in CombatManager.instance.currentAllies)
                        currentAlly.highlightObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetTargets), targetTargets, null);
            }
        }

        #endregion
        
        #region Routines

        private IEnumerator EnemyTurnRoutine()
        {
            var waitDelay = new WaitForSeconds(currentEnemies.Count > 0 ? (1 / currentEnemies.Count) : 0.1f);

            foreach (var currentEnemy in currentEnemies)
            {
                yield return currentEnemy.StartCoroutine(nameof(EnemyExample.ActionRoutine));
                yield return waitDelay;
            }

            CurrentCombatState = CombatState.AllyTurn;
        }
        

        #endregion

    }
}