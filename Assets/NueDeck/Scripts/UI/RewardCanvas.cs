using System;
using System.Collections.Generic;
using NueDeck.Scripts.Data.Containers;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace NueDeck.Scripts.UI
{
    public class RewardCanvas : MonoBehaviour
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
                    //rewardDescription = $"{rewardGold} gold";
                    rewardClone.BuildReward(rewardData.goldReward.rewardSprite,rewardData.goldReward.rewardDescription);
                    rewardClone.rewardButton.onClick.AddListener(()=>GetGoldReward(rewardClone,rewardGold));
                    break;
                case RewardType.Card:
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

        private void GetCardReward(RewardContainer rewardContainer)
        {
            
        }
        
    }
}