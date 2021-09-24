using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Enums;

namespace NueDeck.Scripts.Card
{
    public class CardActionParameters
    {
        public float value;
        public EnemyBase enemyBase;

        public CardActionParameters(float value,EnemyBase enemyBase)
        {
            this.value = value;
            this.enemyBase = enemyBase;
        }
    }
    public abstract class CardActionBase
    {
        protected CardActionBase(){}
        public abstract CardActionType ActionType { get;}
        public abstract void DoAction(CardActionParameters actionParameters);
        
    }
    
    
   
}