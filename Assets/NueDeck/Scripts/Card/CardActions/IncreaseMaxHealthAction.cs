using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.Card.CardActions
{
    public class IncreaseMaxHealthAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.IncreaseMaxHealth;
        public override void DoAction(CardActionParameters actionParameters)
        {
            var newTarget = actionParameters.TargetCharacter
                ? actionParameters.TargetCharacter
                : actionParameters.SelfCharacter;
            
            if (!newTarget) return;
            
            newTarget.CharacterStats.IncreaseMaxHealth(Mathf.RoundToInt(actionParameters.Value));
            
            FxManager.Instance.PlayFx(newTarget.transform,FxType.Buff);
            AudioManager.Instance.PlayOneShot(actionParameters.CardData.audioType);
        }
    }
}