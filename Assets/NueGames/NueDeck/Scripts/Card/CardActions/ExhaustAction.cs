using NueGames.NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Card.CardActions
{
    //Thanks to Borjan#1804
    public class ExhaustAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.Exhaust;
        public override void DoAction(CardActionParameters actionParameters)
        {
            actionParameters.CardBase.Exhaust(false);
        }
    }
}