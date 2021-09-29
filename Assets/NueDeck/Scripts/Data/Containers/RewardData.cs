using System;
using System.Collections.Generic;
using NueDeck.Scripts.Data.Collection;
using NueDeck.Scripts.Managers;
using NueExtentions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NueDeck.Scripts.Data.Containers
{
    [CreateAssetMenu(fileName = "Reward Container", menuName = "Data/Containers/Reward", order = 4)]
    public class RewardData : ScriptableObject
    {
        public CardReward cardReward;
        public GoldReward goldReward;
        

        public CardData GetRandomCardReward()
        {
            return cardReward.rewardCardList.RandomItem();
        }

        public int GetRandomGoldReward()
        {
            return Random.Range(goldReward.minGold, goldReward.maxGold);
        }
    }

    [Serializable]
    public class RewardBase
    {
        public Sprite rewardSprite;
        [TextArea]
        public string rewardDescription;
    }
    
    [Serializable]
    public class CardReward : RewardBase
    {
        public List<CardData> rewardCardList;
    }
    
    [Serializable]
    public class GoldReward : RewardBase
    {
        public int minGold;
        public int maxGold;
    }
    
}