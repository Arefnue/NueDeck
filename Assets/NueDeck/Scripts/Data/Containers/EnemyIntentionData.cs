using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Data.Containers
{
    [CreateAssetMenu(fileName = "Enemy Intention", menuName = "Data/Containers/EnemyIntention", order = 0)]
    public class EnemyIntentionData : ScriptableObject
    {
        public EnemyIntentions enemyIntention;
        public Sprite intentionSprite;
    }
}