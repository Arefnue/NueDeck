using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Card.CardActions
{
    public class DrawAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.Draw;
        public override void DoAction(CardActionParameters actionParameters)
        {
            Debug.Log("Draw");
        }
    }
}