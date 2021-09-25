using System;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.UI;

namespace NueDeck.Scripts.Characters
{
    public class CharacterHealth
    { 
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int CurrentBlock { get; set; }
        public int CurrentPoison { get; set; }

        public Action OnDeath;

        private CharacterCanvas _characterCanvas;

        public CharacterHealth(int maxHealth,CharacterCanvas characterCanvas)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            CurrentBlock = 0;
            CurrentPoison = 0;
            _characterCanvas = characterCanvas;
        }

        public void SetCurrentHealth(int targetCurrentHealth)
        {
            CurrentHealth = targetCurrentHealth <=0 ? 1 : targetCurrentHealth;
            _characterCanvas.UpdateHealthText(CurrentHealth,MaxHealth);
            UIManager.instance.informationCanvas.SetHealthText(CurrentHealth,MaxHealth);
        } 
        
        public void Heal(int value)
        {
            CurrentHealth += value;
            if (CurrentHealth>MaxHealth)  CurrentHealth = MaxHealth;
            _characterCanvas.UpdateHealthText(CurrentHealth,MaxHealth);
            UIManager.instance.informationCanvas.SetHealthText(CurrentHealth,MaxHealth);
        }
        public void Damage(int value, bool canPierceArmor = false)
        {
            var remainingDamage = 0;

            if (!canPierceArmor)
            {
                CurrentBlock -= value;

                if (CurrentBlock < 0)
                {
                    remainingDamage = CurrentBlock * -1;
                    CurrentBlock = 0;
                    _characterCanvas.ClearStatus(StatusType.Block);
                }
                _characterCanvas.UpdateStatusText(StatusType.Block,CurrentBlock);
                UIManager.instance.informationCanvas.SetHealthText(CurrentHealth,MaxHealth);
            }
            else
            {
                remainingDamage = value;
            }
           
           
            CurrentHealth -= remainingDamage;
            
           
            
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
                OnDeath?.Invoke();
            }
            
            _characterCanvas.UpdateHealthText(CurrentHealth,MaxHealth);
            UIManager.instance.informationCanvas.SetHealthText(CurrentHealth,MaxHealth);
        }

        public void ApplyPoison(int value)
        {
            CurrentPoison += value;
            _characterCanvas.ApplyStatus(StatusType.Poison,value);
        } 
        
        public void DamagePoison()
        {
            if (CurrentPoison<=0) return;
            Damage(CurrentPoison,true);
            CurrentPoison--;
            if (CurrentPoison < 0)
            {
                CurrentPoison = 0;
                _characterCanvas.ClearStatus(StatusType.Poison);
            }
            _characterCanvas.UpdateStatusText(StatusType.Poison,CurrentPoison);
        }

        public void EarnBlock(int value)
        {
            CurrentBlock += value;
           _characterCanvas.ApplyStatus(StatusType.Block,value);
        }

        public void IncreaseMaxHealth(int value)
        {
            MaxHealth += value;
           _characterCanvas.UpdateHealthText(CurrentHealth,MaxHealth);
        } 
    }
}