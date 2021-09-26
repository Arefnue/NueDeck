using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Card.CardActions
{
    public class IncreaseStrengthAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.IncreaseStrength;
        public override void DoAction(CardActionParameters actionParameters)
        {
            if (actionParameters.targetCharacter)
            {
                actionParameters.targetCharacter.CharacterStats.ApplyStatus(StatusType.Strength,Mathf.RoundToInt(actionParameters.value));
            }
            else
            {
                actionParameters.selfCharacter.CharacterStats.ApplyStatus(StatusType.Strength,Mathf.RoundToInt(actionParameters.value));
            }
        }
    }
}