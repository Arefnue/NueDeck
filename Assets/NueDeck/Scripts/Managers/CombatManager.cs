using System;
using System.Collections;
using System.Collections.Generic;
using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Characters.Enemies;
using NueDeck.Scripts.Collection;
using NueDeck.Scripts.Enums;
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
        
        private void OnCombatStarted()
        {
            BuildEnemies();
            BuildAllies();
            
            CollectionManager.instance.SetGameDeck();
            CollectionManager.instance.choiceParent.gameObject.SetActive(false);
            UIManager.instance.gameCanvas.SetActive(true);
            
            CurrentCombatState = CombatState.AllyTurn;
        }
        
        private void ExecuteCombatState(CombatState targetState)
        {
            switch (targetState)
            {
                case CombatState.PrepareCombat:
                    break;
                case CombatState.AllyTurn:
                    
                    CollectionManager.instance.currentMana = CollectionManager.instance.maxMana;
                    CollectionManager.instance.DrawCards(CollectionManager.instance.drawCount);
                    foreach (var currentEnemy in currentEnemies) currentEnemy.ShowNextAbility();
                    CollectionManager.instance.canSelectCards = true;
                    
                    break;
                case CombatState.EnemyTurn:
                    
                    CollectionManager.instance.DiscardHand();
                    StartCoroutine(nameof(EnemyTurnRoutine));
                    CollectionManager.instance.canSelectCards = false;
                    
                    break;
                case CombatState.EndCombat:
                    
                    CollectionManager.instance.canSelectCards = false;
                    
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
            for (var i = 0; i < LevelManager.instance.levelAllyList.Count; i++)
            {
                var clone = Instantiate(LevelManager.instance.levelAllyList[i], allyPosList.Count >= i ? allyPosList[i] : allyPosList[0]);
                currentAllies.Add(clone);
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