using System.Collections.Generic;
using NueDeck.Scripts.Card;
using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Data.Characters;
using NueDeck.Scripts.Data.Collection;
using UnityEngine;

namespace NueDeck.Scripts.Data.Settings
{
    [CreateAssetMenu(fileName = "Gameplay Data", menuName = "Data/Settings/GameplayData", order = 0)]
    public class GameplayData : ScriptableObject
    {
        [Header("Gameplay Settings")] 
        public int drawCount = 4;
        public int maxMana = 3;
        public bool canUseCards = true;
        public bool canSelectCards = true;
        public bool isRandomHand = false;
        public List<AllyBase> allyList;
        
        [Header("Decks")] 
        public DeckData initalDeck;
        public int randomCardCount;
        public int maxCardOnHand;
        
        [Header("Card Settings")] 
        public List<CardData> allCardsList;
        public CardObject cardPrefab;

        [Header("Customization Settings")]
        public string defaultName = "Nue";
        public bool useStageSystem;
    }
}