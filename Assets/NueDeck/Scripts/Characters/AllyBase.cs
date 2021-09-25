using NueDeck.Scripts.Data.Characters;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Interfaces;
using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.Characters
{
    public abstract class AllyBase : CharacterBase,IAlly
    {
        public AllyCanvas allyCanvas;
        public AllyData allyData;
       
        public override void Awake()
        {
           
            base.Awake();
        }
        
        public override void BuildCharacter()
        {
            base.BuildCharacter();
            CharacterHealth = new CharacterHealth(allyData.maxHealth,allyCanvas);
            
        }
        
        protected override void OnDeath()
        {
            base.OnDeath();
            CombatManager.instance.OnAllyDeath(this);
        }
        
    }
}