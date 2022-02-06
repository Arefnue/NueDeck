using System.Collections.Generic;
using UnityEngine;

namespace NueDeck.Scripts.Data.Collection
{
    [CreateAssetMenu(fileName = "Deck Data", menuName = "Data/Collection/Deck", order = 1)]
    public class DeckData : ScriptableObject
    {
        [SerializeField] private string deckId;
        
        [SerializeField] private List<CardData> cardList;
        public List<CardData> CardList => cardList;

        public string DeckId => deckId;
    }
}