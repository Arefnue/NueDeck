﻿using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.EnemyBehaviour.EnemyActions
{
    public class EnemyAttackAction: EnemyActionBase
    {
        public override EnemyActionType ActionType => EnemyActionType.Attack;
        
        public override void DoAction(EnemyActionParameters actionParameters)
        {
            Debug.Log("Enemy attacked");
        }
    }
}