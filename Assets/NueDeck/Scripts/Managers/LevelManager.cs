using System;
using System.Collections;
using System.Collections.Generic;
using NueDeck.Scripts.Card;
using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Characters.Allies;
using NueDeck.Scripts.Characters.Enemies;
using NueDeck.Scripts.Collection;
using NueDeck.Scripts.EnemyBehaviour;
using NueDeck.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NueDeck.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;

        public enum LevelState
        {
            Prepare,
            PlayerTurn,
            EnemyTurn,
            Finished
        }

        [Header("Settings")] public Camera mainCam;
        public LayerMask selectableLayer;
        public PlayerExample playerExample;

        public Transform playerPos;
        public List<Transform> enemyPosList;
        public SoundProfileData finalSoundProfileData;

        [Header("Level")] public List<EnemyExample> levelEnemyList;
        public bool isFinalLevel;

        [HideInInspector] public List<EnemyExample> currentEnemies = new List<EnemyExample>();


        public LevelState CurrentLevelState
        {
            get => _currentLevelState;
            set
            {
                ExecuteLevelState(value);

                _currentLevelState = value;
            }
        }

        private LevelState _currentLevelState;

        #region Setup

        private void Awake()
        {
            instance = this;
            BuildEnemies();
            CardActionProcessor.Initialize();
            EnemyActionProcessor.Initialize();
            CurrentLevelState = LevelState.Prepare;
        }

        private void Start()
        {
            OnLevelStart();
        }


        private void ExecuteLevelState(LevelState value)
        {
            switch (value)
            {
                case LevelState.Prepare:
                    break;
                case LevelState.PlayerTurn:


                    CollectionManager.instance.currentMana = CollectionManager.instance.maxMana;
                    CollectionManager.instance.DrawCards(CollectionManager.instance.drawCount);

                    foreach (var currentEnemy in currentEnemies) currentEnemy.ShowNextAction();

                    CollectionManager.instance.canSelectCards = true;

                    break;
                case LevelState.EnemyTurn:

                    CollectionManager.instance.DiscardHand();

                    StartCoroutine(nameof(EnemyTurnRoutine));
                    CollectionManager.instance.canSelectCards = false;

                    break;
                case LevelState.Finished:

                    CollectionManager.instance.canSelectCards = false;

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        #endregion

        #region Public Methods

        public void EndTurn()
        {
            CurrentLevelState = LevelState.EnemyTurn;
        }
        
        public void OnPlayerDeath()
        {
            LoseGame();
        }

        public void LoseGame()
        {
            CurrentLevelState = LevelState.Finished;
            CollectionManager.instance.DiscardHand();
            CollectionManager.instance.discardPile.Clear();
            CollectionManager.instance.drawPile.Clear();
            CollectionManager.instance.handPile.Clear();
            CollectionManager.instance.handController.hand.Clear();
            UIManager.instance.gameCanvas.SetActive(false);
        }

        #endregion

        #region Private Methods

        private void BuildEnemies()
        {
            for (var i = 0; i < levelEnemyList.Count; i++)
            {
                var clone = Instantiate(levelEnemyList[i], enemyPosList.Count >= i ? enemyPosList[i] : enemyPosList[0]);
                currentEnemies.Add(clone);
            }
        }

        private void OnFinal()
        {
            CurrentLevelState = LevelState.Finished;
            CollectionManager.instance.DiscardHand();
            CollectionManager.instance.discardPile.Clear();
            CollectionManager.instance.drawPile.Clear();
            CollectionManager.instance.handPile.Clear();
            CollectionManager.instance.handController.hand.Clear();
            UIManager.instance.gameCanvas.SetActive(false);
          
        }

        private void OnChoiceStart()
        {
            CurrentLevelState = LevelState.Finished;
            
            foreach (var choice in CollectionManager.instance.choicesList) choice.DetermineChoice();
            CollectionManager.instance.DiscardHand();
            CollectionManager.instance.discardPile.Clear();
            CollectionManager.instance.drawPile.Clear();
            CollectionManager.instance.handPile.Clear();
            CollectionManager.instance.handController.hand.Clear();
            CollectionManager.instance.choiceParent.gameObject.SetActive(true);
            UIManager.instance.gameCanvas.SetActive(false);
        }

        private void OnLevelStart()
        {
            if (isFinalLevel)
            {
                StartCoroutine("FinalSfxRoutine");
            }

            CollectionManager.instance.SetGameDeck();
            CollectionManager.instance.choiceParent.gameObject.SetActive(false);
            UIManager.instance.gameCanvas.SetActive(true);
            CurrentLevelState = LevelState.PlayerTurn;
        }

        #endregion

        #region Routines

        private IEnumerator EnemyTurnRoutine()
        {
            var waitDelay = new WaitForSeconds(1 / currentEnemies.Count);

            foreach (var currentEnemy in currentEnemies)
            {
                yield return currentEnemy.StartCoroutine(nameof(EnemyExample.ActionRoutine));
                yield return waitDelay;
            }

            CurrentLevelState = LevelState.PlayerTurn;
        }

        private IEnumerator FinalSfxRoutine()
        {
            while (CurrentLevelState != LevelState.Finished)
            {
                yield return new WaitForSeconds(Random.Range(5, 15));
                AudioManager.instance.PlayOneShot(finalSoundProfileData.GetRandomClip());
            }
        }

        #endregion
    }
}