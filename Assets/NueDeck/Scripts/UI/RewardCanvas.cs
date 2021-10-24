using System;
using System.Collections.Generic;
using NueDeck.Scripts.Data.Containers;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Managers;
using NueExtentions;
using UnityEngine;

namespace NueDeck.Scripts.UI
{
    public class RewardCanvas : CanvasBase
    {
        [SerializeField] private RewardData rewardData;
        [SerializeField] private Transform rewardRoot;
        [SerializeField] private RewardContainer rewardContainerPrefab;

        private List<RewardContainer> _currentRewardsList = new List<RewardContainer>();

        public void BuildReward(RewardType rewardType)
        {
            var rewardClone = Instantiate(rewardContainerPrefab, rewardRoot);
            _currentRewardsList.Add(rewardClone);
            //var rewardDescription = "";
            switch (rewardType)
            {
                case RewardType.Gold:
                    var rewardGold = rewardData.GetRandomGoldReward();
                    rewardClone.BuildReward(rewardData.goldReward.rewardSprite,rewardData.goldReward.rewardDescription);
                    rewardClone.rewardButton.onClick.AddListener(()=>GetGoldReward(rewardClone,rewardGold));
                    break;
                case RewardType.Card:
                    rewardClone.BuildReward(rewardData.cardReward.rewardSprite,rewardData.cardReward.rewardDescription);
                    rewardClone.rewardButton.onClick.AddListener(()=>GetCardReward(rewardClone,1));
                    break;
                case RewardType.Relic:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rewardType), rewardType, null);
            }
        }


        private void GetGoldReward(RewardContainer rewardContainer,int amount)
        {
            GameManager.instance.PersistentGameplayData.CurrentGold += amount;
            _currentRewardsList.Remove(rewardContainer);
            Destroy(rewardContainer.gameObject);
        }

        private void GetCardReward(RewardContainer rewardContainer,int amount)
        {
            GameManager.instance.PersistentGameplayData.CurrentCardsList.Add(GameManager.instance.GameplayData.allCardsList.RandomItem());
            _currentRewardsList.Remove(rewardContainer);
            Destroy(rewardContainer.gameObject);
        }

        public override void ResetCanvas()
        {
            foreach (var rewardContainer in _currentRewardsList)
            {
                Destroy(rewardContainer.gameObject);
            }
            _currentRewardsList.Clear();
        }
    }
}