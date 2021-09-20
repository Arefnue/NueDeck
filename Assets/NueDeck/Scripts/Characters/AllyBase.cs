using NueDeck.Scripts.Interfaces;
using NueDeck.Scripts.Managers;

namespace NueDeck.Scripts.Characters
{
    public abstract class AllyBase : CharacterBase,IAlly
    {
        public override void Awake()
        {
            base.Awake();
        }
        
        
        protected override void OnDeath()
        {
            base.OnDeath();
            CombatManager.instance.OnAllyDeath(this);
        }
        
    }
}