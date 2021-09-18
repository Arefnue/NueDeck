using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.EnemyBehaviour.EnemyActions
{
    public class EnemyPoisonAction : EnemyActionBase
    {
        public override EnemyActionType ActionType => EnemyActionType.Poison;
        public override void DoAction(EnemyActionParameters actionParameters)
        {
            Debug.Log("Ally poisoned");
        }
    }
}