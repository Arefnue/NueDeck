using NueGames.NueDeck.Scripts.Characters;
using NueGames.NueDeck.Scripts.Enums;

namespace NueGames.NueDeck.Scripts.EnemyBehaviour
{
    
    public abstract class EnemyActionBase
    {
        protected EnemyActionBase(){}
        public abstract EnemyActionType ActionType { get;}
        public abstract void DoAction(EnemyActionParameters actionParameters);
        
    }
    
    public class EnemyActionParameters
    {
        public readonly float Value;
        public readonly CharacterBase TargetCharacter;
        public readonly CharacterBase SelfCharacter;

        public EnemyActionParameters(float value,CharacterBase target, CharacterBase self)
        {
            Value = value;
            TargetCharacter = target;
            SelfCharacter = self;
        }
    }
    
    
}