using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Card.CardActions
{
    public class BlockAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.Block;
        public override void DoAction(CardActionParameters actionParameters)
        {
            if (actionParameters.targetCharacter != null)
            {
                actionParameters.targetCharacter.CharacterHealth.EarnBlock(Mathf.RoundToInt(actionParameters.value));
            }
            else
            {
                actionParameters.selfCharacter.CharacterHealth.EarnBlock(Mathf.RoundToInt(actionParameters.value));
            }
        }
    }
}