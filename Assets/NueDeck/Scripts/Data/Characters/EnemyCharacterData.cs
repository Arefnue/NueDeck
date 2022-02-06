using System;
using System.Collections.Generic;
using NueDeck.Scripts.Data.Containers;
using NueDeck.Scripts.Data.Settings;
using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Data.Characters
{
    [CreateAssetMenu(fileName = "Enemy Character Data",menuName = "Data/Characters/Enemy",order = 1)]
    public class EnemyCharacterData : CharacterDataBase
    {
        [Header("Enemy Defaults")] 
        [SerializeField] private List<EnemyAbilityData> enemyAbilityList;
        public List<EnemyAbilityData> EnemyAbilityList => enemyAbilityList;
    }
    
    [Serializable]
    public class EnemyAbilityData
    {
        [Header("Settings")]
        [SerializeField] private string name;
        [SerializeField] private ActionTarget abilityTarget;
        [SerializeField] private EnemyIntentionData intention;
        [SerializeField] private SoundProfileData soundProfile;
        [SerializeField] private bool hideActionValue;
        [SerializeField] private List<EnemyActionData> actionList;
        public string Name => name;
        public ActionTarget AbilityTarget => abilityTarget;
        public EnemyIntentionData Intention => intention;
        public List<EnemyActionData> ActionList => actionList;
        public SoundProfileData SoundProfile => soundProfile;
        public bool HideActionValue => hideActionValue;
    }
    
    [Serializable]
    public class EnemyActionData
    {
        [SerializeField] private EnemyActionType actionType;
        [SerializeField] private float actionValue;
        public EnemyActionType ActionType => actionType;
        public float ActionValue => actionValue;
    }
    
    
    
}