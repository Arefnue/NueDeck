using System.Collections.Generic;
using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Data.Characters;
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
        
    }
}