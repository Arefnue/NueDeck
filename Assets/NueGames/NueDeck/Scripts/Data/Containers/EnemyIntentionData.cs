using NueGames.NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Data.Containers
{
    [CreateAssetMenu(fileName = "Enemy Intention", menuName = "NueDeck/Containers/EnemyIntention", order = 0)]
    public class EnemyIntentionData : ScriptableObject
    {
        [SerializeField] private EnemyIntentionType enemyIntentionType;
        [SerializeField] private Sprite intentionSprite;

        public EnemyIntentionType EnemyIntentionType => enemyIntentionType;

        public Sprite IntentionSprite => intentionSprite;
    }
}