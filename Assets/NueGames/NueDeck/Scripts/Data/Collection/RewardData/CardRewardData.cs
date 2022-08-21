using System.Collections.Generic;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Data.Collection.RewardData
{
    [CreateAssetMenu(fileName = "Card Reward Data",menuName = "NueDeck/Collection/Rewards/CardRW",order = 0)]
    public class CardRewardData : RewardDataBase
    {
        [SerializeField] private List<CardData> rewardCardList;
        public List<CardData> RewardCardList => rewardCardList;
    }
}