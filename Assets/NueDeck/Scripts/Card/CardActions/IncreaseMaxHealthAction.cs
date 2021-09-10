using UnityEngine;

namespace NueDeck.Scripts.Card.CardActions
{
    public class IncreaseMaxHealthAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.IncreaseMaxHealth;
        public override void DoAction(CardActionParameters actionParameters)
        {
            Debug.Log("Increase Max Health");
        }
    }
}