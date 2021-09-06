using NueDeck.Scripts.Controllers;
using UnityEngine;

namespace NueDeck.Scripts.Card
{
    public abstract class ActionSO : ScriptableObject
    {
         public abstract void PlayCard(EnemyBase targetEnemy);
         public abstract void DisposeCard(EnemyBase targetEnemy);
    }
}