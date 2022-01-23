using System;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.Card.CardActions
{
    public class AttackAction: CardActionBase
    {
        public override CardActionType ActionType => CardActionType.Attack;
        public override void DoAction(CardActionParameters actionParameters)
        {
            if (!actionParameters.TargetCharacter) return;
            
            actionParameters.TargetCharacter.CharacterStats.Damage(Mathf.RoundToInt(actionParameters.Value +
                actionParameters.SelfCharacter.CharacterStats.StatusDict[StatusType.Strength].StatusValue));

            if (FxManager.Instance != null)
                FxManager.Instance.PlayFx(actionParameters.TargetCharacter.transform,FxType.Attack);
           
            if (AudioManager.Instance != null) 
                AudioManager.Instance.PlayOneShot(actionParameters.CardData.AudioType);
        }
    }
}