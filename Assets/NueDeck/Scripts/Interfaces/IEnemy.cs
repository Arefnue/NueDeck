using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Characters.Enemies;
using EnemyBase = NueDeck.Scripts.Characters.EnemyBase;

namespace NueDeck.Scripts.Interfaces
{
    public interface IEnemy
    {
        public void OnCardTargetHighlight();
        public void OnCardOverHighlight();
        public void OnCardPlayedForMe();

        public EnemyBase GetEnemyBase();
    }
}