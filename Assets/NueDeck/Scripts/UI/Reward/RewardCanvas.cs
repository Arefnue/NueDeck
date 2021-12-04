using System;
using System.Collections.Generic;
using NueDeck.Scripts.Card;
using NueDeck.Scripts.Data.Collection;
using NueDeck.Scripts.Data.Containers;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Managers;
using NueExtentions;
using UnityEngine;

namespace NueDeck.Scripts.UI.Reward
{
    public class RewardCanvas : CanvasBase
    {
        [SerializeField] private RewardData rewardData;
        [SerializeField] private Transform rewardRoot;
        [SerializeField] private RewardContainer rewardContainerPrefab;

        [Header("Choice")]
        [SerializeField] private List<Transform> choiceCardSpawnTransformList;
        [SerializeField] private Choice choicePrefab;
        public ChoicePanel choicePanel;
        
        private readonly List<RewardContainer> _currentRewardsList = new List<RewardContainer>();
        private readonly List<Choice> _spawnedChoiceList = new List<Choice>();
        private readonly List<CardData> _cardRewardList = new List<CardData>();
       
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
                    rewardClone.rewardButton.onClick.AddListener(()=>GetCardReward(rewardClone,3));
                    break;
                case RewardType.Relic:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rewardType), rewardType, null);
            }
        }


        private void GetGoldReward(RewardContainer rewardContainer,int amount)
        {
            GameManager.Instance.PersistentGameplayData.CurrentGold += amount;
            _currentRewardsList.Remove(rewardContainer);
            Destroy(rewardContainer.gameObject);
        }

        private void GetCardReward(RewardContainer rewardContainer,int amount = 3)
        {
            choicePanel.gameObject.SetActive(true);

            foreach (var cardData in rewardData.cardReward.rewardCardList)
            {
                _cardRewardList.Add(cardData);
            }
            
            for (int i = 0; i < amount; i++)
            {
                var choice = Instantiate(choicePrefab, choiceCardSpawnTransformList[i]);
                var reward = _cardRewardList.RandomItem();
                choice.BuildReward(reward);
                _cardRewardList.Remove(reward);
                choice.transform.localPosition = Vector3.zero;
                _spawnedChoiceList.Add(choice);
                _currentRewardsList.Remove(rewardContainer);
            }
            
            Destroy(rewardContainer.gameObject);
        }

        public override void ResetCanvas()
        {
            foreach (var rewardContainer in _currentRewardsList)
            {
                Destroy(rewardContainer.gameObject);
            }

            foreach (var choice in _spawnedChoiceList)
            {
                Destroy(choice.gameObject);
            }
            choicePanel.DisablePanel();
            _spawnedChoiceList?.Clear();
            _currentRewardsList?.Clear();
        }
    }
}