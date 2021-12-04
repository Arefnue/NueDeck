using NueDeck.Scripts.Collection;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.Card.CardActions
{
    public class DrawAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.Draw;
        public override void DoAction(CardActionParameters actionParameters)
        {
            CollectionManager.Instance.DrawCards(Mathf.RoundToInt(actionParameters.Value));
            FxManager.Instance.PlayFx(actionParameters.SelfCharacter.transform,FxType.Buff);
            AudioManager.Instance.PlayOneShot(actionParameters.CardData.audioType);
        }
    }
}