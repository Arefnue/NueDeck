using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.Card.CardActions
{
    public class EarnManaAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.EarnMana;
        public override void DoAction(CardActionParameters actionParameters)
        {
            CombatManager.Instance.IncreaseMana(Mathf.RoundToInt(actionParameters.Value));
            FxManager.Instance.PlayFx(actionParameters.SelfCharacter.transform,FxType.Buff);
            AudioManager.Instance.PlayOneShot(actionParameters.CardData.audioType);
        }
    }
}