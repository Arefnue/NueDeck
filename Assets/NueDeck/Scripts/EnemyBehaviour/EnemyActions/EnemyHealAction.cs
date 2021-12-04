using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.EnemyBehaviour.EnemyActions
{
    public class EnemyHealAction : EnemyActionBase
    {
        public override EnemyActionType ActionType => EnemyActionType.Heal;
        public override void DoAction(EnemyActionParameters actionParameters)
        {
            if (actionParameters.TargetCharacter)
            {
                actionParameters.TargetCharacter.CharacterStats.Heal(Mathf.RoundToInt(actionParameters.Value));
            }
            else
            {
                actionParameters.SelfCharacter.CharacterStats.Heal(Mathf.RoundToInt(actionParameters.Value));
            }
        }
    }
}