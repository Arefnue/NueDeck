using System;
using System.Collections.Generic;
using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Data.Settings;

namespace NueDeck.Scripts.Data
{
    [Serializable]
    public class PersistentGameplayData
    {
        public int DrawCount { get; set; }
        public int MAXMana { get; set; }
        public int CurrentMana { get; set; }
        public bool CanUseCards { get; set; }
        public bool CanSelectCards { get; set; }
        public bool IsRandomHand { get; set; }
        
        public List<AllyBase> AllyList { get; set; }

        private readonly GameplayData _gameplayData;
        
        public PersistentGameplayData(GameplayData gameplayData)
        {
            _gameplayData = gameplayData;

            InitData();
        }

        private void InitData()
        {
            DrawCount = _gameplayData.drawCount;
            MAXMana = _gameplayData.maxMana;
            CurrentMana = MAXMana;
            CanUseCards = _gameplayData.canUseCards;
            CanSelectCards = _gameplayData.canSelectCards;
            IsRandomHand = _gameplayData.isRandomHand;
            AllyList = _gameplayData.allyList;
        }
    }
}