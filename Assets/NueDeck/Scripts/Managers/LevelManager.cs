using System;
using System.Collections;
using System.Collections.Generic;
using NueDeck.Scripts.Controllers;
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
        public PlayerController playerController;

        public Transform playerPos;
        public List<Transform> enemyPosList;
        public SoundProfile finalSoundProfile;

        [Header("Level")] public List<EnemyBase> levelEnemyList;
        public bool isFinalLevel;

        [HideInInspector] public List<EnemyBase> currentEnemies = new List<EnemyBase>();


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


                    HandManager.instance.currentMana = HandManager.instance.maxMana;
                    HandManager.instance.DrawCards(HandManager.instance.drawCount);

                    foreach (var currentEnemy in currentEnemies) currentEnemy.ShowNextAction();

                    HandManager.instance.canSelectCards = true;

                    break;
                case LevelState.EnemyTurn:

                    HandManager.instance.DiscardHand();

                    StartCoroutine(nameof(EnemyTurnRoutine));
                    HandManager.instance.canSelectCards = false;

                    break;
                case LevelState.Finished:

                    HandManager.instance.canSelectCards = false;

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
            HandManager.instance.DiscardHand();
            HandManager.instance.discardPile.Clear();
            HandManager.instance.drawPile.Clear();
            HandManager.instance.handPile.Clear();
            HandManager.instance.handController.hand.Clear();
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
            HandManager.instance.DiscardHand();
            HandManager.instance.discardPile.Clear();
            HandManager.instance.drawPile.Clear();
            HandManager.instance.handPile.Clear();
            HandManager.instance.handController.hand.Clear();
            UIManager.instance.gameCanvas.SetActive(false);
          
        }

        private void OnChoiceStart()
        {
            CurrentLevelState = LevelState.Finished;
            
            foreach (var choice in HandManager.instance.choicesList) choice.DetermineChoice();
            HandManager.instance.DiscardHand();
            HandManager.instance.discardPile.Clear();
            HandManager.instance.drawPile.Clear();
            HandManager.instance.handPile.Clear();
            HandManager.instance.handController.hand.Clear();
            HandManager.instance.choiceParent.gameObject.SetActive(true);
            UIManager.instance.gameCanvas.SetActive(false);
        }

        private void OnLevelStart()
        {
            if (isFinalLevel)
            {
                StartCoroutine("FinalSfxRoutine");
                AudioManager.instance.PlayMusic(AudioManager.instance.bossMusic);
            }

            HandManager.instance.SetGameDeck();
            HandManager.instance.choiceParent.gameObject.SetActive(false);
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
                yield return currentEnemy.StartCoroutine(nameof(EnemyBase.ActionRoutine));
                yield return waitDelay;
            }

            CurrentLevelState = LevelState.PlayerTurn;
        }

        private IEnumerator FinalSfxRoutine()
        {
            while (CurrentLevelState != LevelState.Finished)
            {
                yield return new WaitForSeconds(Random.Range(5, 15));
                AudioManager.instance.PlayOneShot(finalSoundProfile.GetRandomClip());
            }
        }

        #endregion
    }
}