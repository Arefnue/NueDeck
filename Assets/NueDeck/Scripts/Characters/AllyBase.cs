using System;
using System.Linq;
using NueDeck.Scripts.Data.Characters;
using NueDeck.Scripts.Interfaces;
using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.Characters
{
    public abstract class AllyBase : CharacterBase,IAlly
    {
        [Header("Ally Base Settings")]
        [SerializeField] private AllyCanvas allyCanvas;
        [SerializeField] private AllyCharacterData allyCharacterData;
        public AllyCanvas AllyCanvas => allyCanvas;
        public AllyCharacterData AllyCharacterData => allyCharacterData;

        public override void Awake()
        {
            base.Awake();
        }
        
        public override void BuildCharacter()
        {
            base.BuildCharacter();
            allyCanvas.InitCanvas();
            CharacterStats = new CharacterStats(allyCharacterData.MaxHealth,allyCanvas);

            if (!GameManager.Instance)
                throw new Exception("There is no GameManager");
            
            if (GameManager.Instance.PersistentGameplayData.CurrentHealthDict.ContainsKey(allyCharacterData.CharacterID))
            {
                CharacterStats.CurrentHealth = GameManager.Instance.PersistentGameplayData.CurrentHealthDict[allyCharacterData.CharacterID];
                CharacterStats.MaxHealth = GameManager.Instance.PersistentGameplayData.MaxHealthDict[allyCharacterData.CharacterID];
            }
            else
            {
                GameManager.Instance.PersistentGameplayData.CurrentHealthDict.Add(allyCharacterData.CharacterID,CharacterStats.CurrentHealth);
                GameManager.Instance.PersistentGameplayData.MaxHealthDict.Add(allyCharacterData.CharacterID,CharacterStats.MaxHealth);
            }
            
            CharacterStats.OnDeath += OnDeath;
            CharacterStats.SetCurrentHealth(CharacterStats.CurrentHealth);
            
            if (CombatManager.Instance != null)
                CombatManager.Instance.OnAllyTurnStarted += CharacterStats.TriggerAllStatus;
        }
        
        protected override void OnDeath()
        {
            base.OnDeath();
            if (CombatManager.Instance != null)
            {
                CombatManager.Instance.OnAllyTurnStarted -= CharacterStats.TriggerAllStatus;
                CombatManager.Instance.OnAllyDeath(this);
            }

            Destroy(gameObject);
        }

        public void OnCardTargetHighlight()
        {
            
        }

        public void OnCardOverHighlight()
        {
            
        }

        public void OnCardPlayedOnMe()
        {
            
        }

        public CharacterBase GetCharacterBase() => this;

    }
}