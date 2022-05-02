using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Card.CardActions
{
    public class LifeStealAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.LifeSteal;
        public override void DoAction(CardActionParameters actionParameters)
        {
            if (!actionParameters.TargetCharacter) return;

            var value = Mathf.RoundToInt(actionParameters.Value +
                                         actionParameters.SelfCharacter.CharacterStats.StatusDict[StatusType.Strength]
                                             .StatusValue);
            actionParameters.TargetCharacter.CharacterStats.Damage(value);
            actionParameters.SelfCharacter.CharacterStats.Heal(value);
            
            if (FxManager.Instance != null)
            {
                FxManager.Instance.PlayFx(actionParameters.TargetCharacter.transform,FxType.Attack);
                FxManager.Instance.PlayFx(actionParameters.SelfCharacter.transform,FxType.Heal);
                FxManager.Instance.SpawnFloatingText(actionParameters.TargetCharacter.TextSpawnRoot,value.ToString());
            }
           
            if (AudioManager.Instance != null) 
                AudioManager.Instance.PlayOneShot(actionParameters.CardData.AudioType);
        }
    }
}