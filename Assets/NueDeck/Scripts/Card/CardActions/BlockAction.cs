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
                actionParameters.targetCharacter.CharacterStats.ApplyStatus(StatusType.Block,Mathf.RoundToInt(actionParameters.value)+actionParameters.targetCharacter.CharacterStats.statusDict[StatusType.Dexterity].StatusValue);
            }
            else
            {
                actionParameters.selfCharacter.CharacterStats.ApplyStatus(StatusType.Block,Mathf.RoundToInt(actionParameters.value)+actionParameters.targetCharacter.CharacterStats.statusDict[StatusType.Dexterity].StatusValue);
            }
        }
    }
}