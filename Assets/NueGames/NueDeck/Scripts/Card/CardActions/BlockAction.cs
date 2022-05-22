using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Card.CardActions
{
    public class BlockAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.Block;
        public override void DoAction(CardActionParameters actionParameters)
        {
            var newTarget = actionParameters.TargetCharacter
                ? actionParameters.TargetCharacter
                : actionParameters.SelfCharacter;
            
            if (!newTarget) return;
            
            newTarget.CharacterStats.ApplyStatus(StatusType.Block,
                Mathf.RoundToInt(actionParameters.Value + actionParameters.SelfCharacter.CharacterStats
                    .StatusDict[StatusType.Dexterity].StatusValue));

            if (FxManager.Instance != null) 
                FxManager.Instance.PlayFx(newTarget.transform, FxType.Block);
            
            if (AudioManager.Instance != null) 
                AudioManager.Instance.PlayOneShot(actionParameters.CardData.AudioType);
        }
    }
}