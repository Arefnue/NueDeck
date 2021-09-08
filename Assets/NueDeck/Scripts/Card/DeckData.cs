using System.Collections.Generic;
using UnityEngine;

namespace NueDeck.Scripts.Card
{
    [CreateAssetMenu(fileName = "Deck", menuName = "Data/Deck", order = 1)]
    public class DeckData : ScriptableObject
    {
        public List<CardData> cards;
    }
}