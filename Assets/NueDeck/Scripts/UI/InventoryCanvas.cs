using System;
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
        [SerializeField] private DeckData deckData;
        

        public TextMeshProUGUI TitleTextField => titleTextField;

        public Transform CardSpawnRoot => cardSpawnRoot;


        public void ChangeTitle(string newTitle) => TitleTextField.text = newTitle;


        public void SetCards(List<CardData> cardDataList)
        {
            foreach (var cardData in cardDataList)
            {
                var cardBase =Instantiate(cardUIPrefab, CardSpawnRoot);
                cardBase.SetCard(cardData);
            }
        }

        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.A))
        //     {
        //         ChangeTitle(deckData.DeckName);
        //         SetCards(deckData.CardList);
        //     }
        // }
    }
}
