using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Card.CardActions
{
    public class HealAction: CardActionBase
    {
        public override CardActionType ActionType => CardActionType.Heal;

        public override void DoAction(CardActionParameters actionParameters)
        {
            Debug.Log("Heal");
        }
    }
}