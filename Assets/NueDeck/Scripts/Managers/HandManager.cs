using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NueDeck.Scripts.Card;
using NueDeck.Scripts.Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NueDeck.Scripts.Managers
{
    public class HandManager : MonoBehaviour
    {
        public static HandManager instance;

        [Header("Gameplay Settings")] 
        public int drawCount = 4;
        public int maxMana = 3;
        public int currentMana = 3;
        
        public bool canUseCards = true;
        public bool canSelectCards = true;
        public bool isRandomHand = false;
        
        [Header("Choice")]
        public Transform choiceParent;
        public List<Choice> choicesList;

        [Header("Hand")]
        public HandController handController;
        public Transform discardTransform;
        public Transform drawTransform;
        
        [Header("Card Settings")]
        public List<CardSO> allCardsList;
        public CardBase cardPrefab;
        
        [Header("Decks")]
        public List<int> myDeckIDList = new List<int>();
        public DeckSO initalDeck;
        
        [HideInInspector] public List<int> sameChoiceContainerList = new List<int>();
        [HideInInspector] public List<int> drawPile = new List<int>();
        [HideInInspector] public List<int> handPile = new List<int>();
        [HideInInspector] public List<int> discardPile = new List<int>();
        
        
        #region Setup

        private void Awake()
        {
            instance = this;
            SetInitalHand();
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.A))
            {
                DrawCards(2);
            }

#endif
        }

        #endregion

        #region Public Methods
        
        public void DrawCards(int targetDrawCount)
        {
            var currentDrawCount = 0;
            var reverseDraw = false;

            for (var i = 0; i < targetDrawCount; i++)
            {
                
                if (drawPile.Count <= 0)
                {
                    var nDrawCount = targetDrawCount - currentDrawCount;
                    if (nDrawCount >= discardPile.Count) nDrawCount = discardPile.Count;
                    ReshuffleDiscardPile();
                    DrawCards(nDrawCount);
                    break;
                }

                var randomCard = drawPile[Random.Range(0, drawPile.Count)];
                var clone = BuildAndGetCard(randomCard, drawTransform);
                handController.AddCardToHand(clone);
                handPile.Add(randomCard);
                drawPile.Remove(randomCard);
                currentDrawCount++;
                UIManager.instance.SetPileTexts();
            }

                
        }

        public void DeactivateCardHighlights()
        {
            foreach (var currentEnemy in LevelManager.instance.currentEnemies)
                currentEnemy.highlightObject.SetActive(false);

            LevelManager.instance.playerController.playerHighlight.SetActive(false);
        }

        public void IncreaseMana(int target)
        {
            currentMana += target;
            UIManager.instance.SetPileTexts();
        }

        public void DiscardHand()
        {
            foreach (var cardBase in handController.hand) cardBase.Discard();
            handController.hand.Clear();
        }
        
        public void ExhaustRandomCard()
        {
            var targetCard = 0;
            if (drawPile.Count > 0)
            {
                targetCard = drawPile[Random.Range(0, drawPile.Count)];
                StartCoroutine(ExhaustCardRoutine(targetCard, drawTransform,
                    LevelManager.instance.currentEnemies[0].transform));
            }
            else if (discardPile.Count > 0)
            {
                targetCard = discardPile[Random.Range(0, discardPile.Count)];
                StartCoroutine(ExhaustCardRoutine(targetCard, discardTransform,
                    LevelManager.instance.currentEnemies[0].transform));
            }
            else if (instance.handPile.Count > 0)
            {
                targetCard = handPile[Random.Range(0, handPile.Count)];
                var tCard = handController.hand[0];
                foreach (var cardBase in handController.hand)
                    if (cardBase.myProfile.myID == targetCard)
                    {
                        tCard = cardBase;
                        break;
                    }

                StartCoroutine(ExhaustCardRoutine(targetCard, tCard.transform,
                    LevelManager.instance.currentEnemies[0].transform));
                handController.hand?.Remove(tCard);
                Destroy(tCard.gameObject);
            }
            else
            {
                LevelManager.instance.LoseGame();
            }

            drawPile?.Remove(targetCard);
            handPile?.Remove(targetCard);
            discardPile?.Remove(targetCard);
            UIManager.instance.SetPileTexts();
        }

        public void DiscardCard(CardBase targetCard)
        {
            handPile.Remove(targetCard.myProfile.myID);
            discardPile.Add(targetCard.myProfile.myID);
            UIManager.instance.SetPileTexts();
        }
        
        public void HighlightCardTarget(CardSO.CardTargets targetTargets)
        {
            switch (targetTargets)
            {
                case CardSO.CardTargets.Enemy:
                    foreach (var currentEnemy in LevelManager.instance.currentEnemies)
                        currentEnemy.highlightObject.SetActive(true);
                    break;
                case CardSO.CardTargets.Player:
                    LevelManager.instance.playerController.playerHighlight.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetTargets), targetTargets, null);
            }
        }
        public void SetGameDeck()
        {
            foreach (var i in myDeckIDList) drawPile.Add(i);
        }

        #endregion

        #region Private Methods
        
        private CardBase BuildAndGetCard(int id,Transform parent)
        {
            var card = allCardsList.FirstOrDefault(x => x.myID == id);
            if (card)
            {
                var clone = Instantiate(cardPrefab, parent);
                clone.SetCard(card);
                return clone;
            }
            return null;
           
        }

        private void SetInitalHand()
        {
            myDeckIDList.Clear();
            if (isRandomHand)
            {
                for (int i = 0; i < 10; i++)
                    myDeckIDList.Add(allCardsList[Random.Range(0,allCardsList.Count)].myID);
            }
            else
            {
                initalDeck.cards.ForEach(x=>myDeckIDList.Add(x.myID));
            }
        }

        private void ReshuffleDiscardPile()
        {
            foreach (var i in discardPile) drawPile.Add(i);
            discardPile.Clear();
        }
        
        private void ReshuffleDrawPile()
        {
            foreach (var i in drawPile) discardPile.Add(i);
            drawPile.Clear();
        }

        #endregion

        #region Routines

        private IEnumerator ExhaustCardRoutine(int targetID, Transform startTransform, Transform endTransform)
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = 0f;

            var card = BuildAndGetCard(targetID, startTransform);
            card.transform.SetParent(endTransform);
            var startPos = card.transform.localPosition;
            var endPos = Vector3.zero;

            var startScale = card.transform.localScale;
            var endScale = Vector3.zero;

            var startRot = transform.localRotation;
            var endRot = Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360);


            while (true)
            {
                timer += Time.deltaTime * 5;

                card.transform.localPosition = Vector3.Lerp(startPos, endPos, timer);
                card.transform.localScale = Vector3.Lerp(startScale, endScale, timer);
                card.transform.localRotation = Quaternion.Lerp(startRot, endRot, timer);

                if (timer >= 1f) break;

                yield return waitFrame;
            }

            Destroy(card.gameObject);
        }

        #endregion


        

       

       
    }
}