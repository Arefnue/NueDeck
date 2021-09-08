using UnityEngine;

namespace NueDeck.Scripts.Card.CardActions
{
    public class HealAction: CardActionBase
    {
        public override CardActionData.PlayerActionType ActionType => CardActionData.PlayerActionType.Heal;

        public override void DoAction()
        {
            Debug.Log("Heal");
        }
    }
}