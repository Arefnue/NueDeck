using NueDeck.Scripts.Controllers;

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