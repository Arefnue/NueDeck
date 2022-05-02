using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Card.CardActions
{
    public class EarnManaAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.EarnMana;
        public override void DoAction(CardActionParameters actionParameters)
        {
            if (CombatManager.Instance != null)
                CombatManager.Instance.IncreaseMana(Mathf.RoundToInt(actionParameters.Value));
            else
                Debug.LogError("There is no CombatManager");

            if (FxManager.Instance != null)
                FxManager.Instance.PlayFx(actionParameters.SelfCharacter.transform, FxType.Buff);
            
            if (AudioManager.Instance != null) 
                AudioManager.Instance.PlayOneShot(actionParameters.CardData.AudioType);
        }
    }
}