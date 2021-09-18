using NueDeck.Scripts.Characters.Enemies;
using NueDeck.Scripts.Enums;

namespace NueDeck.Scripts.Card
{
    public class CardActionParameters
    {
        public float value;
        public EnemyExample enemyExample;

        public CardActionParameters(float value,EnemyExample enemyExample)
        {
            this.value = value;
            this.enemyExample = enemyExample;
        }
    }
    public abstract class CardActionBase
    {
        protected CardActionBase(){}
        public abstract CardActionType ActionType { get;}
        public abstract void DoAction(CardActionParameters actionParameters);
        
    }
    
    
   
}