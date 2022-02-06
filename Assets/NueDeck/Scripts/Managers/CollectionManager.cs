using System.Collections;
using System.Collections.Generic;
using NueDeck.Scripts.Card;
using NueDeck.Scripts.Collection;
using NueDeck.Scripts.Data.Collection;
using UnityEngine;

namespace NueDeck.Scripts.Managers
{
    public class CollectionManager : MonoBehaviour
    {
        public CollectionManager(){}
      
        public static CollectionManager Instance { get; private set; }

        [Header("Controllers")] 
        [SerializeField] private HandController handController;
        
        public List<CardData> DrawPile { get; private set; } = new List<CardData>();
        public List<CardData> HandPile { get; private set; } = new List<CardData>();
        public List<CardData> DiscardPile { get; private set; } = new List<CardData>();
        public HandController HandController => handController;

        #region Setup
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }
        }

        #endregion

        #region Public Methods
        public void DrawCards(int targetDrawCount)
        {
            var currentDrawCount = 0;

            for (var i = 0; i < targetDrawCount; i++)
            {
                if (GameManager.Instance.GameplayData.maxCardOnHand<=HandPile.Count)
                    return;
                
                if (DrawPile.Count <= 0)
                {
                    var nDrawCount = targetDrawCount - currentDrawCount;
                    
                    if (nDrawCount >= DiscardPile.Count) 
                        nDrawCount = DiscardPile.Count;
                    
                    ReshuffleDiscardPile();
                    DrawCards(nDrawCount);
                    break;
                }

                var randomCard = DrawPile[Random.Range(0, DrawPile.Count)];
                var clone = GameManager.Instance.BuildAndGetCard(randomCard, HandController.drawTransform);
                HandController.AddCardToHand(clone);
                HandPile.Add(randomCard);
                DrawPile.Remove(randomCard);
                currentDrawCount++;
                UIManager.Instance.CombatCanvas.SetPileTexts();
            }
            
            foreach (var cardObject in HandController.hand)
                cardObject.UpdateCardText();
        }
        public void DiscardHand()
        {
            foreach (var cardBase in HandController.hand) 
                cardBase.Discard();
            
            HandController.hand.Clear();
        }
        public void ExhaustRandomCard()
        {
            CardData targetCard = null;
            if (DrawPile.Count > 0)
            {
                targetCard = DrawPile[Random.Range(0, DrawPile.Count)];
                StartCoroutine(ExhaustCardRoutine(targetCard, HandController.drawTransform,
                    CombatManager.Instance.CurrentEnemiesList[0].transform));
            }
            else if (DiscardPile.Count > 0)
            {
                targetCard = DiscardPile[Random.Range(0, DiscardPile.Count)];
                StartCoroutine(ExhaustCardRoutine(targetCard, HandController.discardTransform,
                    CombatManager.Instance.CurrentEnemiesList[0].transform));
            }
            else if (Instance.HandPile.Count > 0)
            {
                targetCard = HandPile[Random.Range(0, HandPile.Count)];
                var tCard = HandController.hand[0];
                foreach (var cardBase in HandController.hand)
                    if (cardBase.CardData == targetCard)
                    {
                        tCard = cardBase;
                        break;
                    }

                StartCoroutine(ExhaustCardRoutine(targetCard, tCard.transform,
                    CombatManager.Instance.CurrentEnemiesList[0].transform));
                HandController.hand?.Remove(tCard);
                Destroy(tCard.gameObject);
            }
            else
            {
                //LevelManager.instance.LoseGame();
            }

            DrawPile?.Remove(targetCard);
            HandPile?.Remove(targetCard);
            DiscardPile?.Remove(targetCard);
            UIManager.Instance.CombatCanvas.SetPileTexts();
        }
        public void OnCardDiscarded(CardObject targetCard)
        {
            HandPile.Remove(targetCard.CardData);
            DiscardPile.Add(targetCard.CardData);
            UIManager.Instance.CombatCanvas.SetPileTexts();
        }
        public void OnCardPlayed(CardObject targetCard)
        {
            HandPile.Remove(targetCard.CardData);
            DiscardPile.Add(targetCard.CardData);
            UIManager.Instance.CombatCanvas.SetPileTexts();
            foreach (var cardObject in HandController.hand)
                cardObject.UpdateCardText();
        }
        public void SetGameDeck()
        {
            foreach (var i in GameManager.Instance.PersistentGameplayData.CurrentCardsList) 
                DrawPile.Add(i);
        }
        #endregion

        #region Private Methods
        private void ReshuffleDiscardPile()
        {
            foreach (var i in DiscardPile) 
                DrawPile.Add(i);
            
            DiscardPile.Clear();
        }
        private void ReshuffleDrawPile()
        {
            foreach (var i in DrawPile) 
                DiscardPile.Add(i);
            
            DrawPile.Clear();
        }
        #endregion

        #region Routines

        private IEnumerator ExhaustCardRoutine(CardData targetData, Transform startTransform, Transform endTransform)
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = 0f;

            var card = GameManager.Instance.BuildAndGetCard(targetData, startTransform);
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