using System;
using System.Collections.Generic;
using NueDeck.Scripts.EnemyBehaviour;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Utils;
using UnityEngine;

namespace NueDeck.Scripts.Characters
{
    [CreateAssetMenu(fileName = "Enemy Data",menuName = "Data/Enemy Data",order = 1)]
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