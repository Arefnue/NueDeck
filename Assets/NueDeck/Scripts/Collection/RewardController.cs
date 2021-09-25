using System.Collections.Generic;
using NueDeck.Scripts.Card;
using UnityEngine;

namespace NueDeck.Scripts.Collection
{
    public class RewardController : MonoBehaviour
    {
        [Header("Choice")]
        public Transform choiceParent;
        public List<Choice> choicesList;

        [HideInInspector] public List<CardData> sameChoiceContainerList = new List<CardData>();
    }
}