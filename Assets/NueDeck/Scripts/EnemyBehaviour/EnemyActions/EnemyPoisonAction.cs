using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.EnemyBehaviour.EnemyActions
{
    public class EnemyPoisonAction : EnemyActionBase
    {
        public override EnemyActionType ActionType => EnemyActionType.Poison;
        public override void DoAction(EnemyActionParameters actionParameters)
        {
            if (actionParameters.targetCharacter)
            {
                actionParameters.targetCharacter.CharacterStats.ApplyStatus(StatusType.Poison,Mathf.RoundToInt(actionParameters.value));
            }
            else
            {
                actionParameters.selfCharacter.CharacterStats.ApplyStatus(StatusType.Poison,Mathf.RoundToInt(actionParameters.value));
            }
        }
    }
}