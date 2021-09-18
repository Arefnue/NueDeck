using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Card.CardActions
{
    public class EarnManaAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.EarnMana;
        public override void DoAction(CardActionParameters actionParameters)
        {
            Debug.Log("Earn Mana");
        }
    }
}