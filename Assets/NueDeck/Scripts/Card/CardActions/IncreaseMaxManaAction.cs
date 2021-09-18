using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Card.CardActions
{
    public class IncreaseMaxManaAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.IncreaseMaxMana;
        public override void DoAction(CardActionParameters actionParameters)
        {
            Debug.Log("Increase Max Mana");
        }
    }
}