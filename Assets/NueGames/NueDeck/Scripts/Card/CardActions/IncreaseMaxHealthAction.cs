using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Card.CardActions
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

            if (FxManager.Instance != null) 
                FxManager.Instance.PlayFx(newTarget.transform, FxType.Buff);
            
            if (AudioManager.Instance != null) 
                AudioManager.Instance.PlayOneShot(actionParameters.CardData.AudioType);
        }
    }
}