using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.EnemyBehaviour.EnemyActions
{
    public class EnemyPoisonAction : EnemyActionBase
    {
        public override EnemyActionType ActionType => EnemyActionType.Poison;
        public override void DoAction(EnemyActionParameters actionParameters)
        {
            var newTarget = actionParameters.TargetCharacter;

            if (!newTarget) return;
            
            newTarget.CharacterStats.ApplyStatus(StatusType.Poison,Mathf.RoundToInt(actionParameters.Value));
            
            if (FxManager.Instance != null) 
                FxManager.Instance.PlayFx(newTarget.transform, FxType.Poison);
            
            if (AudioManager.Instance != null) 
                AudioManager.Instance.PlayOneShot(AudioActionType.Poison);
        }
    }
}