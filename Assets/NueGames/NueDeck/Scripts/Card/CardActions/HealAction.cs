using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Card.CardActions
{
    public class HealAction: CardActionBase
    {
        public override CardActionType ActionType => CardActionType.Heal;

        public override void DoAction(CardActionParameters actionParameters)
        {
            var newTarget = actionParameters.TargetCharacter
                ? actionParameters.TargetCharacter
                : actionParameters.SelfCharacter;

            if (!newTarget) return;
            
            newTarget.CharacterStats.Heal(Mathf.RoundToInt(actionParameters.Value));

            if (FxManager.Instance != null) 
                FxManager.Instance.PlayFx(newTarget.transform, FxType.Heal);
            
            if (AudioManager.Instance != null) 
                AudioManager.Instance.PlayOneShot(actionParameters.CardData.AudioType);
        }
    }
}