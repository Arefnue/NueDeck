using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.EnemyBehaviour.EnemyActions
{
    public class EnemyHealAction : EnemyActionBase
    {
        public override EnemyActionType ActionType => EnemyActionType.Heal;
        public override void DoAction(EnemyActionParameters actionParameters)
        {
            var newTarget = actionParameters.TargetCharacter
                ? actionParameters.TargetCharacter
                : actionParameters.SelfCharacter;

            if (!newTarget) return;
            
            newTarget.CharacterStats.Heal(Mathf.RoundToInt(actionParameters.Value));

            if (FxManager.Instance != null) 
                FxManager.Instance.PlayFx(newTarget.transform, FxType.Heal);
            
            if (AudioManager.Instance != null) 
                AudioManager.Instance.PlayOneShot(AudioActionType.Heal);
        }
    }
}