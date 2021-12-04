using NueDeck.Scripts.Data.Collection;
using NueDeck.Scripts.Managers;
using NueDeck.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NueDeck.Scripts.Card
{
    public class Choice : MonoBehaviour,IPointerEnterHandler,IPointerDownHandler,IPointerExitHandler,IPointerUpHandler
    {
        private CardObject _cardObject;
        private Vector3 _initalScale;
        [SerializeField] private float showScaleRate = 1.15f;

        private void Awake()
        {
            _cardObject = GetComponent<CardObject>();
            _initalScale = transform.localScale;
        }

        public void BuildReward(CardData cardData)
        {
            _cardObject.SetCard(cardData);
            _cardObject.UpdateCardText();
        }


        private void OnChoice()
        {
            GameManager.instance.PersistentGameplayData.CurrentCardsList.Add(_cardObject.CardData);
            
            UIManager.instance.rewardCanvas.choicePanel.DisablePanel();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = _initalScale * showScaleRate;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
           
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = _initalScale;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnChoice();
        }
    }
}
