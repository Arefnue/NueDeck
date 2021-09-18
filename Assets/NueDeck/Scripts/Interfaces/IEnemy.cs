using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Characters.Enemies;

namespace NueDeck.Scripts.Interfaces
{
    public interface IEnemy
    {
        public void OnCardTargetHighlight();
        public void OnCardOverHighlight();
        public void OnCardPlayedForMe();

        public EnemyExample GetEnemyBase();
    }
}