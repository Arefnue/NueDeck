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
        [SerializeField] private AllyCanvas allyCanvas;
        [SerializeField] private AllyData allyData;
        public AllyCanvas AllyCanvas => allyCanvas;
        public AllyData AllyData => allyData;

        public override void Awake()
        {
            base.Awake();
        }
        
        public override void BuildCharacter()
        {
            base.BuildCharacter();
            allyCanvas.InitCanvas();
            CharacterStats = new CharacterStats(allyData.maxHealth,allyCanvas);

            if (!GameManager.Instance)
                throw new Exception("There is no GameManager");
            
            if (GameManager.Instance.PersistentGameplayData.CurrentHealthDict.ContainsKey(allyData.characterID))
            {
                CharacterStats.CurrentHealth = GameManager.Instance.PersistentGameplayData.CurrentHealthDict[allyData.characterID];
                CharacterStats.MaxHealth = GameManager.Instance.PersistentGameplayData.MaxHealthDict[allyData.characterID];
            }
            else
            {
                GameManager.Instance.PersistentGameplayData.CurrentHealthDict.Add(allyData.characterID,CharacterStats.CurrentHealth);
                GameManager.Instance.PersistentGameplayData.MaxHealthDict.Add(allyData.characterID,CharacterStats.MaxHealth);
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