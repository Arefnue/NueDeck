using System;
using System.Collections.Generic;
using NueDeck.Scripts.Data.Containers;
using NueDeck.Scripts.Data.Settings;
using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Data.Characters
{
    [CreateAssetMenu(fileName = "Enemy Data",menuName = "Data/Characters/Enemy",order = 1)]
    public class EnemyData : CharacterData
    {
        [Header("Enemy Defaults")]
      
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