using System;
using System.Collections.Generic;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.UI;
using UnityEngine;

namespace NueDeck.Scripts.Characters
{
    public class StatusStats
    {
        public StatusType StatusType { get; set; }
        public int StatusValue { get; set; }

        public Action OnTriggerAction;
        public bool DecreaseOverTurn { get; set; }
        public bool IsPermanent { get; set; }
        public bool IsActive { get; set; }
        public bool CanNegativeStack { get; set; }
        public bool ClearAtNextTurn { get; set; }
        
        public StatusStats(StatusType statusType,int statusValue,bool decreaseOverTurn = false, bool isPermanent = false,bool isActive = false,bool canNegativeStack = false,bool clearAtNextTurn = false)
        {
            StatusType = statusType;
            StatusValue = statusValue;
            DecreaseOverTurn = decreaseOverTurn;
            IsPermanent = isPermanent;
            IsActive = isActive;
            CanNegativeStack = canNegativeStack;
            ClearAtNextTurn = clearAtNextTurn;
        }
    }
    public class CharacterStats
    { 
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
       
        public Action OnDeath;
        public Action<int, int> OnHealthChanged;
        public Action<StatusType,int> OnStatusChanged;
        public Action<StatusType, int> OnStatusApplied;
        public Action<StatusType> OnStatusCleared;
        private CharacterCanvas _characterCanvas;

        public Dictionary<StatusType, StatusStats> statusDict = new Dictionary<StatusType, StatusStats>();
        
        public CharacterStats(int maxHealth, CharacterCanvas characterCanvas)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            SetAllStatus();

            _characterCanvas = characterCanvas;
            OnHealthChanged += _characterCanvas.UpdateHealthText;
            OnStatusChanged += _characterCanvas.UpdateStatusText;
            OnStatusApplied += _characterCanvas.ApplyStatus;
            OnStatusCleared += _characterCanvas.ClearStatus;
        }

        private void SetAllStatus()
        {
            for (int i = 0; i < Enum.GetNames(typeof(StatusType)).Length; i++)
            {
                statusDict.Add((StatusType) i, new StatusStats((StatusType) i, 0));
            }

            statusDict[StatusType.Poison].DecreaseOverTurn = true;
            statusDict[StatusType.Poison].OnTriggerAction += DamagePoison;

            statusDict[StatusType.Block].ClearAtNextTurn = true;

            statusDict[StatusType.Strength].CanNegativeStack = true;
            statusDict[StatusType.Dexterity].CanNegativeStack = true;
        }

        public void ApplyStatus(StatusType targetStatus,int value)
        {
            if (statusDict[targetStatus].IsActive)
            {
                statusDict[targetStatus].StatusValue += value;
                OnStatusChanged?.Invoke(targetStatus, statusDict[targetStatus].StatusValue);
                
            }
            else
            {
                statusDict[targetStatus].StatusValue += value;
                statusDict[targetStatus].IsActive = true;
                OnStatusApplied?.Invoke(targetStatus, statusDict[targetStatus].StatusValue);
            }
            
        }
        
        public void TriggerAllStatus()
        {
            for (int i = 0; i < Enum.GetNames(typeof(StatusType)).Length; i++)
            {
                TriggerStatus((StatusType) i);
            }
           
        }

        public void TriggerStatus(StatusType targetStatus)
        {
            statusDict[targetStatus].OnTriggerAction?.Invoke();
          

            if (statusDict[targetStatus].ClearAtNextTurn)
            {
                ClearStatus(targetStatus);
                return;
            }
            
            if (statusDict[targetStatus].StatusValue <= 0)
            {
                if (statusDict[targetStatus].CanNegativeStack)
                {
                    if (statusDict[targetStatus].StatusValue == 0)
                        if (!statusDict[targetStatus].IsPermanent)
                            ClearStatus(targetStatus);
                }
                else
                {
                    if (!statusDict[targetStatus].IsPermanent)
                        ClearStatus(targetStatus);
                }
            }
            
            if (statusDict[targetStatus].DecreaseOverTurn) 
                statusDict[targetStatus].StatusValue--;
        }

        public void ClearStatus(StatusType targetStatus)
        {
            statusDict[targetStatus].IsActive = false;
            statusDict[targetStatus].StatusValue = 0;
            OnStatusCleared?.Invoke(targetStatus);
        }

        public void SetCurrentHealth(int targetCurrentHealth)
        {
            CurrentHealth = targetCurrentHealth <=0 ? 1 : targetCurrentHealth;
            OnHealthChanged?.Invoke(CurrentHealth,MaxHealth);
        } 
        
        public void Heal(int value)
        {
            CurrentHealth += value;
            if (CurrentHealth>MaxHealth)  CurrentHealth = MaxHealth;
            OnHealthChanged?.Invoke(CurrentHealth,MaxHealth);
        }
        public void Damage(int value, bool canPierceArmor = false)
        {
            var remainingDamage = value;
            
            if (!canPierceArmor)
            {
                if (statusDict[StatusType.Block].IsActive)
                {
                    ApplyStatus(StatusType.Block,-value);
                
                    if (statusDict[StatusType.Block].StatusValue < 0)
                    {
                        remainingDamage = statusDict[StatusType.Block].StatusValue * -1;
                        ClearStatus(StatusType.Block);
                    }
                }
                
            }
            
            CurrentHealth -= remainingDamage;
            
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OnDeath?.Invoke();
            }
            OnHealthChanged?.Invoke(CurrentHealth,MaxHealth);
        }
        
        private void DamagePoison()
        {
            if (statusDict[StatusType.Poison].StatusValue<=0) return;
            Damage(statusDict[StatusType.Poison].StatusValue,true);
        }
        
        public void IncreaseMaxHealth(int value)
        {
            MaxHealth += value;
            OnHealthChanged?.Invoke(CurrentHealth,MaxHealth);
        } 
    }
}