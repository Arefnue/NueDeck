using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Card.CardActions
{
    public class IncreaseMaxHealthAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.IncreaseMaxHealth;
        public override void DoAction(CardActionParameters actionParameters)
        {
            if (actionParameters.targetCharacter)
            {
                actionParameters.targetCharacter.CharacterStats.IncreaseMaxHealth(Mathf.RoundToInt(actionParameters.value));
            }
            else
            {
                actionParameters.selfCharacter.CharacterStats.IncreaseMaxHealth(Mathf.RoundToInt(actionParameters.value));
            }
        }
    }
}