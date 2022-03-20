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
        [SerializeField] private int drawCount = 4;
        [SerializeField] private int maxMana = 3;
        [SerializeField] private bool canUseCards = true;
        [SerializeField] private bool canSelectCards = true;
        [SerializeField] private bool isRandomHand = false;
        [SerializeField] private List<AllyBase> allyList;
        
        [Header("Decks")] 
        [SerializeField] private DeckData initalDeck;
        [SerializeField] private int randomCardCount;
        [SerializeField] private int maxCardOnHand;
        
        [Header("Card Settings")] 
        [SerializeField] private List<CardData> allCardsList;
        [SerializeField] private CardBase cardPrefab;

        [Header("Customization Settings")] 
        [SerializeField] private string defaultName = "Nue";
        [SerializeField] private bool useStageSystem;
        
        #region Encapsulation
        public int DrawCount => drawCount;
        public int MaxMana => maxMana;
        public bool CanUseCards => canUseCards;
        public bool CanSelectCards => canSelectCards;
        public bool IsRandomHand => isRandomHand;
        public List<AllyBase> AllyList => allyList;
        public DeckData InitalDeck => initalDeck;
        public int RandomCardCount => randomCardCount;
        public int MaxCardOnHand => maxCardOnHand;
        public List<CardData> AllCardsList => allCardsList;
        public CardBase CardPrefab => cardPrefab;
        public string DefaultName => defaultName;
        public bool UseStageSystem => useStageSystem;
        #endregion
    }
}