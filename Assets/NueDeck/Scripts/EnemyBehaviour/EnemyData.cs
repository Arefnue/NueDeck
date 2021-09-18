using System;
using System.Collections.Generic;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Utils;
using UnityEngine;

namespace NueDeck.Scripts.EnemyBehaviour
{
    [CreateAssetMenu(fileName = "Enemy Data",menuName = "Data/Enemy Data",order = 1)]
    public class EnemyData : ScriptableObject
    {
       
        [Header("Enemy Defaults")]
        public int myID;
        public string myName;
        public ActionTargets myTargets;
        
        [TextArea]
        public string myDescription;
        public Sprite mySprite;
        public EnemyIntentions myIntention;
        public List<EnemyAction> actionList;
        public SoundProfileData mySoundProfileData;
        
    }

    [Serializable]
    public class EnemyAction
    {
        public CardActionType myPlayerActionType;
        public float value;
        
    }
}