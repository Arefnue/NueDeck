using System;
using System.Collections.Generic;
using NueDeck.Scripts.Data.Containers;
using NueDeck.Scripts.Data.Settings;
using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Data.Collection
{
    [CreateAssetMenu(fileName = "Card Data",menuName = "Data/Collection/Card",order = 0)]
    public class CardData : ScriptableObject
    {
        [Header("Card Defaults")]
        public int myID;
        public ActionTargets myTargets;
        public bool usableWithoutTarget;
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