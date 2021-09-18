using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.EnemyBehaviour.EnemyActions
{
    public class EnemyStealCardFromDeckAction : EnemyActionBase
    {
        public override EnemyActionType ActionType => EnemyActionType.StealCardFromDeck;
        
        public override void DoAction(EnemyActionParameters actionParameters)
        {
            Debug.Log("Card steal");
        }
    }
}