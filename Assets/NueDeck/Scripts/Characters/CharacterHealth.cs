using System;

namespace NueDeck.Scripts.Characters
{
    public class CharacterHealth
    { 
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int CurrentBlock { get; set; }
        public int CurrentPoison { get; set; }

        public Action OnDeath;
        
        public CharacterHealth(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            CurrentBlock = 0;
            CurrentPoison = 0;
        }

        public void SetCurrentHealth(int targetCurrentHealth) => CurrentHealth = targetCurrentHealth <=0 ? 1 : targetCurrentHealth;
        
        public void Heal(int value)
        {
            CurrentHealth += value;
            if (CurrentHealth>MaxHealth)  CurrentHealth = MaxHealth;
           
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
                }
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
        }

        public void ApplyPoison(int value) => CurrentPoison += value;
        
        public void DamagePoison()
        {
            if (CurrentPoison<=0) return;
            Damage(CurrentPoison,true);
            CurrentPoison--;
            if (CurrentPoison <0) CurrentPoison = 0;
          
        }

        public void EarnBlock(int value) => CurrentBlock += value;

        public void IncreaseMaxHealth(int value) => MaxHealth += value;
    }
}