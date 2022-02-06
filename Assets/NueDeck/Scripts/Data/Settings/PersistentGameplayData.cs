using System;
using System.Collections.Generic;
using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Data.Collection;
using NueDeck.Scripts.Managers;
using NueDeck.Scripts.UI;
using UnityEngine;

namespace NueDeck.Scripts.Data.Settings
{
    [Serializable]
    public class PersistentGameplayData
    {
        public int DrawCount
        {
            get => drawCount;
            set => drawCount = value;
        }

        public int MaxMana
        {
            get => maxMana;
            set => maxMana = value;
        }

        public int CurrentMana
        {
            get => currentMana;
            set => currentMana = value;
        }

        public bool CanUseCards
        {
            get => canUseCards;
            set => canUseCards = value;
        }

        public bool CanSelectCards
        {
            get => canSelectCards;
            set => canSelectCards = value;
        }

        public bool IsRandomHand
        {
            get => isRandomHand;
            set => isRandomHand = value;
        }

        public List<AllyBase> AllyList
        {
            get => allyList;
            set => allyList = value;
        }

        public int CurrentStageId
        {
            get => currentStageId;
            set => currentStageId = value;
        }

        public int CurrentEncounterId
        {
            get => currentEncounterId;
            set => currentEncounterId = value;
        }

        public bool IsFinalEncounter
        {
            get => isFinalEncounter;
            set => isFinalEncounter = value;
        }

        public List<CardData> CurrentCardsList
        {
            get => currentCardsList;
            set => currentCardsList = value;
        }

        public int PlayerCurrentHealth
        {
            get => playerCurrentHealth;
            set => playerCurrentHealth = value;
        }

        public int PlayerMaxHealth
        {
            get => playerMaxHealth;
            set => playerMaxHealth = value;
        }

        public int CurrentGold
        {
            get
            {
                return currentGold;
            }
            set
            {
                currentGold = value;
            }
        }


        public void SetPlayerCurrentHealth(int newValue) => playerCurrentHealth = newValue;
        public void SetPlayerMaxHealth(int newValue) => playerMaxHealth = newValue;
        
        private readonly GameplayData _gameplayData;
        
        [SerializeField] private int currentGold;
        [SerializeField] private int drawCount;
        [SerializeField] private int maxMana;
        [SerializeField] private int currentMana;
        [SerializeField] private bool canUseCards;
        [SerializeField] private bool canSelectCards;
        [SerializeField] private bool isRandomHand;
        [SerializeField] private List<AllyBase> allyList;
        [SerializeField] private int currentStageId;
        [SerializeField] private int currentEncounterId;
        [SerializeField] private bool isFinalEncounter;
        [SerializeField] private List<CardData> currentCardsList;
        [SerializeField] private int playerCurrentHealth;
        [SerializeField] private int playerMaxHealth;

        public PersistentGameplayData(GameplayData gameplayData)
        {
            _gameplayData = gameplayData;

            InitData();
        }

        private void InitData()
        {
            DrawCount = _gameplayData.DrawCount;
            MaxMana = _gameplayData.MaxMana;
            CurrentMana = MaxMana;
            CanUseCards = _gameplayData.CanUseCards;
            CanSelectCards = _gameplayData.CanSelectCards;
            IsRandomHand = _gameplayData.IsRandomHand;
            AllyList = _gameplayData.AllyList;
            CurrentEncounterId = 0;
            CurrentStageId = 0;
            CurrentGold = 0;
            CurrentCardsList = new List<CardData>();
            IsFinalEncounter = false;
            PlayerCurrentHealth = -1;
            PlayerMaxHealth = -1;
        }
    }
}