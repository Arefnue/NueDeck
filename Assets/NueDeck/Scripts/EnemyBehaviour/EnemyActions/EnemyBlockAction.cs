using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.EnemyBehaviour.EnemyActions
{
    public class EnemyBlockAction : EnemyActionBase
    {
        public override EnemyActionType ActionType => EnemyActionType.Block;
        
        public override void DoAction(EnemyActionParameters actionParameters)
        {
            if (actionParameters.TargetCharacter)
            {
                actionParameters.TargetCharacter.CharacterStats.ApplyStatus(StatusType.Block,Mathf.RoundToInt(actionParameters.Value)+actionParameters.TargetCharacter.CharacterStats.StatusDict[StatusType.Dexterity].StatusValue);
            }
            else
            {
                actionParameters.SelfCharacter.CharacterStats.ApplyStatus(StatusType.Block,Mathf.RoundToInt(actionParameters.Value)+actionParameters.SelfCharacter.CharacterStats.StatusDict[StatusType.Dexterity].StatusValue);
            }
        }
    }
}