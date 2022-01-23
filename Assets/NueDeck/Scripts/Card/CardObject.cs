using System.Collections;
using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Collection;
using NueDeck.Scripts.Data.Collection;
using NueDeck.Scripts.Managers;
using NueTooltip.Core;
using NueTooltip.CursorSystem;
using NueTooltip.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace NueDeck.Scripts.Card
{
    public class CardObject : MonoBehaviour,I2DTooltipTarget, IPointerDownHandler, IPointerUpHandler
    {
        [Header("References")]
        [SerializeField] private MeshRenderer cardMeshRenderer;
        [SerializeField] private Transform descriptionRoot;
        [SerializeField] private Canvas canvas;
        
        [Header("TextFields")]
        [SerializeField] private TextMeshProUGUI nameTextField;
        [SerializeField] private TextMeshProUGUI descTextField;
        [SerializeField] private TextMeshProUGUI manaTextField;
        
        [Header("Images")]
        [SerializeField] private Image frontImage;
        [SerializeField] private Image backImage;
        [SerializeField] private Image inActiveImage;
        
        public CardData CardData { get; private set; }

        private readonly Vector2 _dissolveOffset = new Vector2(0.1f, 0);
        private readonly Vector2 _dissolveSpeed = new Vector2(2f, 2f);
        private Color _dissolveColor;
        private Color _baseColor;
        private Color _colorOutline;
        private bool _isInactive;
        private Material _cardMaterial;
        private Transform _transform;
        private WaitForEndOfFrame _waitFrame;
        
        #region Setup

        private void Awake()
        {
            _transform = transform;
            _waitFrame = new WaitForEndOfFrame();
        }

        public void SetCard(CardData targetProfile)
        {
            CardData = targetProfile;
            _cardMaterial = cardMeshRenderer.material;

            _baseColor = _cardMaterial.GetColor("_Color");
            _colorOutline = _cardMaterial.GetColor("_OutlineColor");
            _dissolveColor = _cardMaterial.GetColor("_DissolveColor");
            
            nameTextField.text = CardData.CardName;
            descTextField.text = CardData.MyDescription;
            manaTextField.text = CardData.ManaCost.ToString();
            frontImage.sprite = CardData.CardSprite;

            canvas.worldCamera = CollectionManager.Instance.handController.cam;
        }
        #endregion
        
        #region Card Methods

        public void Use(CharacterBase self,CharacterBase target)
        {
            SpendMana(CardData.ManaCost);
            
            foreach (var playerAction in CardData.CardActionDataList)
                CardActionProcessor.GetAction(playerAction.MyPlayerActionType)
                    .DoAction(new CardActionParameters(playerAction.Value,
                        target,self,CardData));
            
            CollectionManager.Instance.OnCardPlayed(this);
            
            StartCoroutine(nameof(DiscardRoutine));
        }
        public void Discard()
        {
            CollectionManager.Instance.OnCardDiscarded(this);
            StartCoroutine(nameof(DiscardRoutine));
        }
        public void Exhaust()
        {
            StartCoroutine(nameof(Dissolve));
        }

        private void SpendMana(int value)
        {
            GameManager.Instance.PersistentGameplayData.CurrentMana -= value;
        }
        
        public void SetInactiveMaterialState(bool isInactive, Material inactiveMaterial = null) 
        {
            if (isInactive == this._isInactive) return; 
            
            _isInactive = isInactive;
            cardMeshRenderer.sharedMaterial = isInactive ? inactiveMaterial : _cardMaterial;

            inActiveImage.gameObject.SetActive(isInactive);
        }
        
        public void UpdateCardText()
        {
            CardData.UpdateDescription();
            nameTextField.text = CardData.CardName;
            descTextField.text = CardData.MyDescription;
            manaTextField.text = CardData.ManaCost.ToString();
        }
        
        #endregion
        
        #region Routines

        private IEnumerator Dissolve() 
        {
            Vector2 t = Vector2.zero - _dissolveOffset;
            while (t.x < 1) {
                t.x = (t.x + Time.deltaTime * _dissolveSpeed.x);
                if (t.y < 1) {
                    t.y = (t.y + Time.deltaTime * _dissolveSpeed.y);
                }
                _cardMaterial.SetVector("_Dissolve", t);
                _cardMaterial.SetColor("_DissolveColor", _dissolveColor * 4 * t.y);
                yield return _waitFrame;
            }
            Destroy(gameObject);
        }

        private IEnumerator DiscardRoutine()
        {
           
            var timer = 0f;
            
            transform.SetParent(CollectionManager.Instance.handController.discardTransform);
            
            var startPos = _transform.localPosition;
            var endPos = Vector3.zero;

            var startScale = _transform.localScale;
            var endScale = Vector3.zero;

            var startRot = _transform.localRotation;
            var endRot = Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360);
            
            while (true)
            {
                timer += Time.deltaTime*5;

                _transform.localPosition = Vector3.Lerp(startPos, endPos, timer);
                _transform.localRotation = Quaternion.Lerp(startRot,endRot,timer);
                _transform.localScale = Vector3.Lerp(startScale, endScale, timer);
                
                if (timer>=1f)  break;
                
                yield return _waitFrame;
            }
            
            Destroy(gameObject);
        }

        #endregion

        #region Pointer Events

        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowTooltipInfo();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideTooltipInfo(TooltipManager.Instance);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            HideTooltipInfo(TooltipManager.Instance);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ShowTooltipInfo();
        }

        #endregion

        #region Tooltip
        public void ShowTooltipInfo()
        {
            var tooltipManager = TooltipManager.Instance;
            foreach (var cardDataSpecialKeyword in CardData.KeywordsList)
            {
                var specialKeyword = tooltipManager.SpecialKeywordData.SpecialKeywordBaseList.Find(x=>x.SpecialKeyword == cardDataSpecialKeyword);
                if (specialKeyword != null)
                    ShowTooltipInfo(tooltipManager,specialKeyword.GetContent(),specialKeyword.GetHeader(),descriptionRoot,CursorType.Default,CollectionManager.Instance.handController.cam);
            }
        }
        public void ShowTooltipInfo(TooltipManager tooltipManager, string content, string header = "", Transform tooltipStaticTransform = null, CursorType targetCursor = CursorType.Default,Camera cam = null, float delayShow =0)
        {
            tooltipManager.ShowTooltip(content,header,tooltipStaticTransform,targetCursor,cam,delayShow);
        }

        public void HideTooltipInfo(TooltipManager tooltipManager)
        {
            tooltipManager.HideTooltip();
        }
        
        #endregion
       
    }
}