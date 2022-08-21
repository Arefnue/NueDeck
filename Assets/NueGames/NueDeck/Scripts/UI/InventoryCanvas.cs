using System.Collections.Generic;
using NueGames.NueDeck.Scripts.Card;
using NueGames.NueDeck.Scripts.Data.Collection;
using NueGames.NueDeck.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NueGames.NueDeck.Scripts.UI
{
    public class InventoryCanvas : CanvasBase
    {
        [SerializeField] private TextMeshProUGUI titleTextField;
        [SerializeField] private LayoutGroup cardSpawnRoot;
        [SerializeField] private CardBase cardUIPrefab;

        public TextMeshProUGUI TitleTextField => titleTextField;
        public LayoutGroup CardSpawnRoot => cardSpawnRoot;

        private List<CardBase> _spawnedCardList = new List<CardBase>();

        public void ChangeTitle(string newTitle) => TitleTextField.text = newTitle;
        
        public void SetCards(List<CardData> cardDataList)
        {
            var count = 0;
            for (int i = 0; i < _spawnedCardList.Count; i++)
            {
                count++;
                if (i>=cardDataList.Count)
                {
                    _spawnedCardList[i].gameObject.SetActive(false);
                }
                else
                {
                    _spawnedCardList[i].SetCard(cardDataList[i],false);
                    _spawnedCardList[i].gameObject.SetActive(true);
                }
                
            }
            
            var cal = cardDataList.Count - count;
            if (cal>0)
            {
                for (var i = 0; i < cal; i++)
                {
                    var cardData = cardDataList[count+i];
                    var cardBase = Instantiate(cardUIPrefab, CardSpawnRoot.transform);
                    cardBase.SetCard(cardData, false);
                    _spawnedCardList.Add(cardBase);
                }
            }
            
           
        }

        public override void OpenCanvas()
        {
            base.OpenCanvas();
            if (CollectionManager)
                CollectionManager.HandController.DisableDragging();
        }

        public override void CloseCanvas()
        {
            base.CloseCanvas();
            if (CollectionManager)
                CollectionManager.HandController.EnableDragging();
        }

        public override void ResetCanvas()
        {
            base.ResetCanvas();
        }
    }
}
