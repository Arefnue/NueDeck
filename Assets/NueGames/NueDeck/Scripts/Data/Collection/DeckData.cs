using System.Collections.Generic;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Data.Collection
{
    [CreateAssetMenu(fileName = "Deck Data", menuName = "NueDeck/Collection/Deck", order = 1)]
    public class DeckData : ScriptableObject
    {
        [SerializeField] private string deckId;
        [SerializeField] private string deckName;

        [SerializeField] private List<CardData> cardList;
        public List<CardData> CardList => cardList;

        public string DeckId => deckId;

        public string DeckName => deckName;
    }
}