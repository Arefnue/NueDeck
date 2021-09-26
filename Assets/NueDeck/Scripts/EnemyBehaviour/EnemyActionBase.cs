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
        public CharacterBase targetCharacter;
        public CharacterBase selfCharacter;

        public EnemyActionParameters(float value,CharacterBase target, CharacterBase self)
        {
            this.value = value;
            targetCharacter = target;
            selfCharacter = self;
        }
    }
    
    
}