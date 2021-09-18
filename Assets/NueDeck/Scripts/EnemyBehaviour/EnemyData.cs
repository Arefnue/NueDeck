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
        public int enemyID;
        public string enemyName;
        [TextArea]
        public string enemyDescription;
        public Sprite enemySprite;
        public List<EnemyAbilityData> enemyAbilityList;

    }
    
    [Serializable]
    public class EnemyAbilityData
    {
        public string name;
        public ActionTargets abilityTarget;
        public EnemyIntentionData intention;
        public List<EnemyActionData> actionList;
        public SoundProfileData soundProfile;
        public bool hideActionValue;
    }
    
    [Serializable]
    public class EnemyActionData
    {
        public EnemyActionType enemyActionType;
        public float value;

    }
    
    
    
}