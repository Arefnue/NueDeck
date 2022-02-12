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
            
            var data = GameManager.Instance.PersistentGameplayData.AllyHealthDataList.Find(x =>
                x.CharacterId == AllyCharacterData.CharacterID);
            
            if (data != null)
            {
                CharacterStats.CurrentHealth = data.CurrentHealth;
                CharacterStats.MaxHealth = data.MaxHealth;
            }
            else
            {
                GameManager.Instance.PersistentGameplayData.SetAllyHealthData(AllyCharacterData.CharacterID,CharacterStats.CurrentHealth,CharacterStats.MaxHealth);
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
    }

    [Serializable]
    public class AllyHealthData
    {
        [SerializeField] private string characterId;
        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;
        
        public int MaxHealth
        {
            get => maxHealth;
            set => maxHealth = value;
        }

        public int CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value;
        }

        public string CharacterId
        {
            get => characterId;
            set => characterId = value;
        }
    }
}