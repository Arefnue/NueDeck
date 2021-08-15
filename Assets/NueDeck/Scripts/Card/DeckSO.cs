using System.Collections.Generic;
using UnityEngine;

namespace NueDeck.Scripts.Card
{
    [CreateAssetMenu(fileName = "Deck", menuName = "Data/Deck", order = 0)]
    public class DeckSO : ScriptableObject
    {
        public List<CardSO> cards;
    }
}