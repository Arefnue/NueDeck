using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.EnemyBehaviour.EnemyActions
{
    public class EnemyHealAction : EnemyActionBase
    {
        public override EnemyActionType ActionType => EnemyActionType.Heal;
        public override void DoAction(EnemyActionParameters actionParameters)
        {
            if (actionParameters.targetCharacter)
            {
                actionParameters.targetCharacter.CharacterStats.Heal(Mathf.RoundToInt(actionParameters.value));
            }
            else
            {
                actionParameters.selfCharacter.CharacterStats.Heal(Mathf.RoundToInt(actionParameters.value));
            }
        }
    }
}