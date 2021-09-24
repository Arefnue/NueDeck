using System;
using System.Collections.Generic;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Utils;
using UnityEngine;

namespace NueDeck.Scripts.Card
{
    [CreateAssetMenu(fileName = "Card Data",menuName = "Data/Card Data",order = 0)]
    public class CardData : ScriptableObject
    {
        [Header("Card Defaults")]
        public int myID;
        public ActionTargets myTargets;
        public int myManaCost;
        public string myName;
        [TextArea]
        public string myDescription;
        public Sprite mySprite;
        public List<CardActionData> actionList;
        public SoundProfileData mySoundProfileData;
        
    }

    [Serializable]
    public class CardActionData
    {
        public CardActionType myPlayerActionType;
        public float value;
        
    }
}