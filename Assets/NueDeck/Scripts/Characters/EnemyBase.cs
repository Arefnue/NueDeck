using NueDeck.Scripts.Characters.Enemies;
using NueDeck.Scripts.Interfaces;
using UnityEngine;

namespace NueDeck.Scripts.Characters
{
    public class EnemyBase : CharacterBase, IEnemy
    {
        public void OnCardTargetHighlight()
        {
            
        }

        public void OnCardOverHighlight()
        {
            
        }

        public void OnCardPlayedForMe()
        {
            
        }

        public EnemyBase GetEnemyBase() => this;

    }
}