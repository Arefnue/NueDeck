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
            if (CollectionManager.Instance != null)
                CollectionManager.Instance.DrawCards(Mathf.RoundToInt(actionParameters.Value));
            else
                Debug.LogError("There is no CollectionManager");
            
            if (FxManager.Instance != null)
                FxManager.Instance.PlayFx(actionParameters.SelfCharacter.transform, FxType.Buff);

            if (AudioManager.Instance != null) 
                AudioManager.Instance.PlayOneShot(actionParameters.CardData.AudioType);
        }
    }
}