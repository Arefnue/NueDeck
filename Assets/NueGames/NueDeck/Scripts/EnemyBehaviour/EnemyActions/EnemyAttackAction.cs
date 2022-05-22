using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.EnemyBehaviour.EnemyActions
{
    public class EnemyAttackAction: EnemyActionBase
    {
        public override EnemyActionType ActionType => EnemyActionType.Attack;
        
        public override void DoAction(EnemyActionParameters actionParameters)
        {
            if (!actionParameters.TargetCharacter) return;
            var value = Mathf.RoundToInt(actionParameters.Value +
                                         actionParameters.SelfCharacter.CharacterStats.StatusDict[StatusType.Strength]
                                             .StatusValue);
            actionParameters.TargetCharacter.CharacterStats.Damage(value);
            if (FxManager.Instance != null)
            {
                FxManager.Instance.PlayFx(actionParameters.TargetCharacter.transform,FxType.Attack);
                FxManager.Instance.SpawnFloatingText(actionParameters.TargetCharacter.TextSpawnRoot,value.ToString());
            }

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayOneShot(AudioActionType.Attack);
           
        }
    }
}