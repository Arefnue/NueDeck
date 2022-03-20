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
        [Header("References")]
        [SerializeField] private RewardData rewardData;
        [SerializeField] private Transform rewardRoot;
        [SerializeField] private RewardContainer rewardContainerPrefab;

        [Header("Choice")] 
        [SerializeField] private bool use3DCard;
        [SerializeField] private List<Transform> choiceCardSpawnTransformList;
        [SerializeField] private Transform choice2DCardSpawnRoot;
        [SerializeField] private ChoiceCard choiceCardPrefab;
        [SerializeField] private ChoicePanel choicePanel;
        
        private readonly List<RewardContainer> _currentRewardsList = new List<RewardContainer>();
        private readonly List<ChoiceCard> _spawnedChoiceList = new List<ChoiceCard>();
        private readonly List<CardData> _cardRewardList = new List<CardData>();

        public ChoicePanel ChoicePanel => choicePanel;
        
        #region Public Methods
        public void BuildReward(RewardType rewardType)
        {
            var rewardClone = Instantiate(rewardContainerPrefab, rewardRoot);
            _currentRewardsList.Add(rewardClone);
            //var rewardDescription = "";
            switch (rewardType)
            {
                case RewardType.Gold:
                    var rewardGold = rewardData.GetRandomGoldReward();
                    rewardClone.BuildReward(rewardData.GoldReward.RewardSprite,rewardData.GoldReward.RewardDescription);
                    rewardClone.RewardButton.onClick.AddListener(()=>GetGoldReward(rewardClone,rewardGold));
                    break;
                case RewardType.Card:
                    rewardClone.BuildReward(rewardData.CardReward.RewardSprite,rewardData.CardReward.RewardDescription);
                    rewardClone.RewardButton.onClick.AddListener(()=>GetCardReward(rewardClone,3));
                    break;
                case RewardType.Relic:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rewardType), rewardType, null);
            }
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
            ChoicePanel.DisablePanel();
            _spawnedChoiceList?.Clear();
            _currentRewardsList?.Clear();
        }

        #endregion
        
        #region Private Methods
        private void GetGoldReward(RewardContainer rewardContainer,int amount)
        {
            GameManager.Instance.PersistentGameplayData.CurrentGold += amount;
            _currentRewardsList.Remove(rewardContainer);
            UIManager.Instance.InformationCanvas.SetGoldText(GameManager.Instance.PersistentGameplayData.CurrentGold);
            Destroy(rewardContainer.gameObject);
        }

        private void GetCardReward(RewardContainer rewardContainer,int amount = 3)
        {
            ChoicePanel.gameObject.SetActive(true);

            foreach (var cardData in rewardData.CardReward.RewardCardList)
                _cardRewardList.Add(cardData);
            
            for (int i = 0; i < amount; i++)
            {
                Transform spawnTransform = use3DCard ? choiceCardSpawnTransformList[i] : choice2DCardSpawnRoot;
              
                var choice = Instantiate(choiceCardPrefab, spawnTransform);
                
                var reward = _cardRewardList.RandomItem();
                choice.BuildReward(reward);
                
                _cardRewardList.Remove(reward);
                _spawnedChoiceList.Add(choice);
                _currentRewardsList.Remove(rewardContainer);
                
                if (use3DCard)
                    choice.transform.localPosition = Vector3.zero;
            }
            
            Destroy(rewardContainer.gameObject);
        }
        #endregion
        
    }
}