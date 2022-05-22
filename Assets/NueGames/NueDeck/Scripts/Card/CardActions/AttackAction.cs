using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Card.CardActions
{
    public class AttackAction: CardActionBase
    {
        public override CardActionType ActionType => CardActionType.Attack;
        public override void DoAction(CardActionParameters actionParameters)
        {
            if (!actionParameters.TargetCharacter) return;
            
            var value = actionParameters.Value +
                        actionParameters.SelfCharacter.CharacterStats.StatusDict[StatusType.Strength].StatusValue;
            actionParameters.TargetCharacter.CharacterStats.Damage(Mathf.RoundToInt(value));

            if (FxManager.Instance != null)
            {
                FxManager.Instance.PlayFx(actionParameters.TargetCharacter.transform,FxType.Attack);
                FxManager.Instance.SpawnFloatingText(actionParameters.TargetCharacter.TextSpawnRoot,value.ToString());
            }
           
            if (AudioManager.Instance != null) 
                AudioManager.Instance.PlayOneShot(actionParameters.CardData.AudioType);
        }
    }
}