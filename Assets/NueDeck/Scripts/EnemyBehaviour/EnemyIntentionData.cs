using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.EnemyBehaviour
{
    [CreateAssetMenu(fileName = "Enemy Intention", menuName = "Data/Enemy Intention", order = 2)]
    public class EnemyIntentionData : ScriptableObject
    {
        public EnemyIntentions enemyIntention;
        public Sprite intentionSprite;
    }
}