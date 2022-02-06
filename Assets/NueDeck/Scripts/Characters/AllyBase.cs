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
            
            if (GameManager.Instance.PersistentGameplayData.PlayerCurrentHealth != -1)
            {
                CharacterStats.CurrentHealth = GameManager.Instance.PersistentGameplayData.PlayerCurrentHealth;
                CharacterStats.MaxHealth = GameManager.Instance.PersistentGameplayData.PlayerMaxHealth;
            }
            else
            {
                GameManager.Instance.PersistentGameplayData.SetPlayerCurrentHealth(CharacterStats.CurrentHealth); 
                GameManager.Instance.PersistentGameplayData.SetPlayerMaxHealth(CharacterStats.MaxHealth);
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