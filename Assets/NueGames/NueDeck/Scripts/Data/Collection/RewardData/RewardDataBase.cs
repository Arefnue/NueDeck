using UnityEngine;

namespace NueGames.NueDeck.Scripts.Data.Collection.RewardData
{
    public class RewardDataBase : ScriptableObject
    {
        [SerializeField] private Sprite rewardSprite;
        [TextArea] [SerializeField] private string rewardDescription;
        public Sprite RewardSprite => rewardSprite;
        public string RewardDescription => rewardDescription;
    }
}