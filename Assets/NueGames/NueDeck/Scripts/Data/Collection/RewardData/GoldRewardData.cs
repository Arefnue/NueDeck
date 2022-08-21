using UnityEngine;

namespace NueGames.NueDeck.Scripts.Data.Collection.RewardData
{
    [CreateAssetMenu(fileName = "Gold Reward Data",menuName = "NueDeck/Collection/Rewards/GoldRW",order = 0)]
    public class GoldRewardData : RewardDataBase
    {
        [SerializeField] private int minGold;
        [SerializeField] private int maxGold;
        public int MinGold => minGold;
        public int MaxGold => maxGold;
    }
}