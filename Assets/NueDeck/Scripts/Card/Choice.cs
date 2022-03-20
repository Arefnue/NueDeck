using NueDeck.Scripts.Data.Collection;
using NueDeck.Scripts.Managers;
using NueDeck.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NueDeck.Scripts.Card
{
    public class Choice : MonoBehaviour,IPointerEnterHandler,IPointerDownHandler,IPointerExitHandler,IPointerUpHandler
    {
        [SerializeField] private float showScaleRate = 1.15f;
        private CardBase _cardBase;
        private Vector3 _initalScale;

        private void Awake()
        {
            _cardBase = GetComponent<CardBase>();
            _initalScale = transform.localScale;
        }

        public void BuildReward(CardData cardData)
        {
            _cardBase.SetCard(cardData);
            _cardBase.UpdateCardText();
        }


        private void OnChoice()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.PersistentGameplayData.CurrentCardsList.Add(_cardBase.CardData);

            if (UIManager.Instance != null)
                UIManager.Instance.RewardCanvas.ChoicePanel.DisablePanel();
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
