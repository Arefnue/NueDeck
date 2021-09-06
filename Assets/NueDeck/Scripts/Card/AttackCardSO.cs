using NueDeck.Scripts.Controllers;
using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.Card
{
    [CreateAssetMenu(fileName = "Attack Card", menuName = "Cards/Attack", order = 0)]
    public class AttackCardSO : ActionSO
    {
        public float attackValue;
        
        public override void PlayCard(EnemyBase targetEnemy)
        {
            FxManager.instance.PlayFx(targetEnemy.fxParent,FxManager.FxType.Attack);
        }

        public override void DisposeCard(EnemyBase targetEnemy)
        {
            
        }
        
    }
}