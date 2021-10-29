using System.Collections;
using System.Collections.Generic;
using NueDeck.Scripts.Card;
using NueDeck.Scripts.Data;
using NueDeck.Scripts.Data.Collection;
using NueDeck.Scripts.Managers;
using NueDeck.Scripts.UI;
using UnityEngine;

namespace NueDeck.Scripts.Collection
{
    public class CollectionManager : MonoBehaviour
    {
        public static CollectionManager instance;

        [Header("Controllers")] 
        public HandController handController;
        public RewardController rewardController;
        
        [HideInInspector] public List<CardData> drawPile = new List<CardData>();
        [HideInInspector] public List<CardData> handPile = new List<CardData>();
        [HideInInspector] public List<CardData> discardPile = new List<CardData>();

        #region Setup

        private void Awake()
        {
            instance = this;
        }

        #endregion

        #region Public Methods

        public void DrawCards(int targetDrawCount)
        {
            var currentDrawCount = 0;

            for (var i = 0; i < targetDrawCount; i++)
            {
                if (GameManager.instance.GameplayData.maxCardOnHand<=handPile.Count)
                    return;
                
                if (drawPile.Count <= 0)
                {
                    var nDrawCount = targetDrawCount - currentDrawCount;
                    if (nDrawCount >= discardPile.Count) nDrawCount = discardPile.Count;
                    ReshuffleDiscardPile();
                    DrawCards(nDrawCount);
                    break;
                }

                var randomCard = drawPile[Random.Range(0, drawPile.Count)];
                var clone = GameManager.instance.BuildAndGetCard(randomCard, handController.drawTransform);
                handController.AddCardToHand(clone);
                handPile.Add(randomCard);
                drawPile.Remove(randomCard);
                currentDrawCount++;
                UIManager.instance.combatCanvas.SetPileTexts();
            }
            
            foreach (var cardObject in handController.hand)
            {
                cardObject.UpdateCardText();
            }
        }

        public void DiscardHand()
        {
            foreach (var cardBase in handController.hand) cardBase.Discard();
            handController.hand.Clear();
        }

        public void ExhaustRandomCard()
        {
            CardData targetCard = null;
            if (drawPile.Count > 0)
            {
                targetCard = drawPile[Random.Range(0, drawPile.Count)];
                StartCoroutine(ExhaustCardRoutine(targetCard, handController.drawTransform,
                    CombatManager.instance.currentEnemies[0].transform));
            }
            else if (discardPile.Count > 0)
            {
                targetCard = discardPile[Random.Range(0, discardPile.Count)];
                StartCoroutine(ExhaustCardRoutine(targetCard, handController.discardTransform,
                    CombatManager.instance.currentEnemies[0].transform));
            }
            else if (instance.handPile.Count > 0)
            {
                targetCard = handPile[Random.Range(0, handPile.Count)];
                var tCard = handController.hand[0];
                foreach (var cardBase in handController.hand)
                    if (cardBase.CardData == targetCard)
                    {
                        tCard = cardBase;
                        break;
                    }

                StartCoroutine(ExhaustCardRoutine(targetCard, tCard.transform,
                    CombatManager.instance.currentEnemies[0].transform));
                handController.hand?.Remove(tCard);
                Destroy(tCard.gameObject);
            }
            else
            {
                //LevelManager.instance.LoseGame();
            }

            drawPile?.Remove(targetCard);
            handPile?.Remove(targetCard);
            discardPile?.Remove(targetCard);
            UIManager.instance.combatCanvas.SetPileTexts();
        }

        public void OnCardDiscarded(CardObject targetCard)
        {
            handPile.Remove(targetCard.CardData);
            discardPile.Add(targetCard.CardData);
            UIManager.instance.combatCanvas.SetPileTexts();
        }

        public void OnCardPlayed(CardObject targetCard)
        {
            handPile.Remove(targetCard.CardData);
            discardPile.Add(targetCard.CardData);
            UIManager.instance.combatCanvas.SetPileTexts();
            foreach (var cardObject in handController.hand)
            {
                cardObject.UpdateCardText();
            }
        }

        public void SetGameDeck()
        {
            foreach (var i in GameManager.instance.PersistentGameplayData.CurrentCardsList) 
                drawPile.Add(i);
        }

        #endregion

        #region Private Methods
        
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

        private IEnumerator ExhaustCardRoutine(CardData targetData, Transform startTransform, Transform endTransform)
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = 0f;

            var card = GameManager.instance.BuildAndGetCard(targetData, startTransform);
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