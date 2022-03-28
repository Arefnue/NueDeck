using System.Collections.Generic;
using NueDeck.Scripts.Card;
using NueDeck.Scripts.Data.Collection;
using TMPro;
using UnityEngine;

namespace NueDeck.Scripts.UI
{
    public class InventoryCanvas : CanvasBase
    {
        [SerializeField] private TextMeshProUGUI titleTextField;
        [SerializeField] private Transform cardSpawnRoot;
        [SerializeField] private CardBase cardUIPrefab;

        public TextMeshProUGUI TitleTextField => titleTextField;

        public Transform CardSpawnRoot => cardSpawnRoot;


        public void ChangeTitle(string newTitle) => titleTextField.text = newTitle;


        public void SetCards(List<CardData> cardDataList)
        {
            foreach (var cardData in cardDataList)
            {
                
            }
        }
        

    }
}
