using System.Collections.Generic;
using NueDeck.Scripts.Characters;
using UnityEngine;

namespace NueDeck.Scripts.Gameplay
{
    [CreateAssetMenu(fileName = "Gameplay Data", menuName = "Data/Gameplay Data", order = 5)]
    public class GameplayData : ScriptableObject
    {
        [Header("Gameplay Settings")] 
        public int drawCount = 4;
        public int maxMana = 3;
        public bool canUseCards = true;
        public bool canSelectCards = true;
        public bool isRandomHand = false;
        public List<AllyBase> allyList;
    }
}