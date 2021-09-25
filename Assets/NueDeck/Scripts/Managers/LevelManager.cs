using System;
using System.Collections.Generic;
using NueDeck.Scripts.Card;
using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Collection;
using NueDeck.Scripts.EnemyBehaviour;
using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        
        [Header("Settings")] 
        public Camera mainCam;
        
        [Header("Level")] 
        public List<EnemyBase> levelEnemyList;
        
        #region Setup
        private void Awake()
        {
            instance = this;
           
            CardActionProcessor.Initialize();
            EnemyActionProcessor.Initialize();
           
        }

        private void Start()
        {
            CombatManager.instance.StartCombat();
        }

        #endregion

        #region Public Methods
        public void LoseGame()
        {
            CombatManager.instance.CurrentCombatState = CombatState.EndCombat;
            CollectionManager.instance.DiscardHand();
            CollectionManager.instance.discardPile.Clear();
            CollectionManager.instance.drawPile.Clear();
            CollectionManager.instance.handPile.Clear();
            CollectionManager.instance.handController.hand.Clear();
            UIManager.instance.gameCanvas.SetActive(false);
        }

        #endregion

        #region Private Methods
        private void OnChoiceStart()
        {
            CombatManager.instance.CurrentCombatState = CombatState.EndCombat;
            
            foreach (var choice in CollectionManager.instance.rewardController.choicesList) choice.DetermineChoice();
            CollectionManager.instance.DiscardHand();
            CollectionManager.instance.discardPile.Clear();
            CollectionManager.instance.drawPile.Clear();
            CollectionManager.instance.handPile.Clear();
            CollectionManager.instance.handController.hand.Clear();
            CollectionManager.instance.rewardController.choiceParent.gameObject.SetActive(true);
            UIManager.instance.gameCanvas.SetActive(false);
        }
        
        #endregion

      
    }
}