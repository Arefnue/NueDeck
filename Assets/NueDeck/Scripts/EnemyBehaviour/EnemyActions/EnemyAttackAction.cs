using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.EnemyBehaviour.EnemyActions
{
    public class EnemyAttackAction: EnemyActionBase
    {
        public override EnemyActionType ActionType => EnemyActionType.Attack;
        
        public override void DoAction(EnemyActionParameters actionParameters)
        {
            if (!actionParameters.TargetCharacter) return;
           
            actionParameters.TargetCharacter.CharacterStats.Damage(Mathf.RoundToInt(actionParameters.Value+actionParameters.SelfCharacter.CharacterStats.StatusDict[StatusType.Strength].StatusValue));
            FxManager.Instance.PlayFx(actionParameters.TargetCharacter.transform,FxType.Attack);
            AudioManager.Instance.PlayOneShot(AudioActionType.Attack);
        }
    }
}