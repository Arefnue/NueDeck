using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Enums;


namespace NueDeck.Scripts.EnemyBehaviour
{
    
    public abstract class EnemyActionBase
    {
        protected EnemyActionBase(){}
        public abstract EnemyActionType ActionType { get;}
        public abstract void DoAction(EnemyActionParameters actionParameters);
        
    }
    
    public class EnemyActionParameters
    {
        public float value;
        public AllyBase targetAlly;

        public EnemyActionParameters(float value,AllyBase targetAlly)
        {
            this.value = value;
            this.targetAlly = targetAlly;
        }
    }
    
    
}