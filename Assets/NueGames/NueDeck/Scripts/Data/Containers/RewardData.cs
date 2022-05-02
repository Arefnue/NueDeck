using System;
using System.Collections.Generic;
using NueGames.NueDeck.Scripts.Data.Collection;
using NueGames.NueDeck.Scripts.NueExtentions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NueGames.NueDeck.Scripts.Data.Containers
{
    [CreateAssetMenu(fileName = "Reward Container", menuName = "Data/Containers/Reward", order = 4)]
    public class RewardData : ScriptableObject
    {
        [SerializeField] private CardReward cardReward;
        [SerializeField] private GoldReward goldReward;
        public CardReward CardReward => cardReward;
        public GoldReward GoldReward => goldReward;
        public CardData GetRandomCardReward() => CardReward.RewardCardList.RandomItem();
        public int GetRandomGoldReward() => Random.Range(GoldReward.MinGold, GoldReward.MaxGold);
       
    }

    [Serializable]
    public class RewardBase
    {
        [SerializeField] private Sprite rewardSprite;
        [TextArea] [SerializeField] private string rewardDescription;
        public Sprite RewardSprite => rewardSprite;
        public string RewardDescription => rewardDescription;
    }
    
    [Serializable]
    public class CardReward : RewardBase
    {
        [SerializeField] private List<CardData> rewardCardList;
        public List<CardData> RewardCardList => rewardCardList;
    }
    
    [Serializable]
    public class GoldReward : RewardBase
    {
        [SerializeField] private int minGold;
        [SerializeField] private int maxGold;
        public int MinGold => minGold;
        public int MaxGold => maxGold;
    }
    
}