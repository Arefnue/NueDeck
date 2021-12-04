using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.EnemyBehaviour.EnemyActions
{
    public class EnemyPoisonAction : EnemyActionBase
    {
        public override EnemyActionType ActionType => EnemyActionType.Poison;
        public override void DoAction(EnemyActionParameters actionParameters)
        {
            if (actionParameters.TargetCharacter)
            {
                actionParameters.TargetCharacter.CharacterStats.ApplyStatus(StatusType.Poison,Mathf.RoundToInt(actionParameters.Value));
            }
            else
            {
                actionParameters.SelfCharacter.CharacterStats.ApplyStatus(StatusType.Poison,Mathf.RoundToInt(actionParameters.Value));
            }
        }
    }
}