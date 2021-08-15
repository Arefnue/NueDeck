using System.Collections;
using NueDeck.Scripts.Controllers;
using NueDeck.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace NueDeck.Scripts.Card
{
    public class CardBase : MonoBehaviour
    {
        [Header("Components")]
        public MeshRenderer meshRenderer;
        public Material material;
        
        [Header("Texts")] 
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descText;
        public TextMeshProUGUI manaText;
        public Image frontImage;
        public Image backImage;

        public Sprite hideSprite;
        [HideInInspector]public CardSO myProfile;
        
        private Vector2 _dissolveOffset = new Vector2(0.1f, 0);
        private Vector2 _dissolveSpeed = new Vector2(2f, 2f);
        private Color _dissolveColor;
        private Color _color;
        private Color _color2;
        
        private bool isInactive;

        #region Setup
        public void SetCard()
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
            material = meshRenderer.material; // Create material instance

            _color = material.GetColor("_Color");
            _color2 = material.GetColor("_OutlineColor");
            _dissolveColor = material.GetColor("_DissolveColor");
            
            nameText.text = myProfile.myName;
            descText.text = myProfile.myDescription;
            manaText.text = myProfile.myManaCost.ToString();
            frontImage.sprite = myProfile.mySprite;

            if (LevelManager.instance.malfunctionController.currentMalfunction.myMalfunctionType == MalfunctionBase.MalfunctionType.VisualDisorder)
            {
                if (Random.value<=0.25f)
                {
                    Hide();
                }
            }
        }

        #endregion
        
        #region CardActions

        public void UseCard(EnemyBase targetEnemy = null)
        {
            SpendMana(myProfile.myManaCost);
            foreach (var playerAction in myProfile.playerActionList)
            {
                CardActions.PlayCardAction(targetEnemy, playerAction);
                AudioManager.instance.PlayOneShot(playerAction.mySoundProfile.GetRandomClip());
            }
            HandManager.instance.DiscardCard(this);
            StartCoroutine("DiscardRoutine");
        }
        public void Discard()
        {
            HandManager.instance.DiscardCard(this);
            StartCoroutine("DiscardRoutine");
        }
        public void Exhaust()
        {
            StartCoroutine("Dissolve");
        }

        public void Hide()
        {
            nameText.text = "cardProfile.name";
            descText.text = "cardProfile.description";
            manaText.text = "cardProfile.manaCost";
            frontImage.sprite = hideSprite;
        }

        public void Show()
        {
            nameText.text = myProfile.myName;
            descText.text = myProfile.myDescription;
            manaText.text = myProfile.myManaCost.ToString();
            frontImage.sprite = myProfile.mySprite;
        }

        public void SpendMana(int value)
        {
            if (LevelManager.instance.malfunctionController.currentMalfunction.myMalfunctionType == MalfunctionBase.MalfunctionType.SuicidalViolence)
            {
                LevelManager.instance.playerController.myHealth.TakeDamage(value);
            }
            else
            {
                HandManager.instance.currentMana -= value;
            }
           
        }
        
        
        #endregion
        
        #region Routines

        protected IEnumerator Dissolve() {
            Vector2 t = Vector2.zero - _dissolveOffset;
            while (t.x < 1) {
                t.x = (t.x + Time.deltaTime * _dissolveSpeed.x);
                if (t.y < 1) {
                    t.y = (t.y + Time.deltaTime * _dissolveSpeed.y);
                }
                material.SetVector("_Dissolve", t);
                material.SetColor("_DissolveColor", _dissolveColor * 4 * t.y);
                yield return null;
            }
            Destroy(gameObject);
        }

        private IEnumerator DiscardRoutine()
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = 0f;
            
            transform.SetParent(HandManager.instance.discardTransform);
            
            var startPos = transform.localPosition;
            var endPos = Vector3.zero;

            var startScale = transform.localScale;
            var endScale = Vector3.zero;

            var startRot = transform.localRotation;
            var endRot = Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360);
            
            while (true)
            {

                timer += Time.deltaTime*5;

                transform.localPosition = Vector3.Lerp(startPos, endPos, timer);
                transform.localScale = Vector3.Lerp(startScale, endScale, timer);
                transform.localRotation = Quaternion.Lerp(startRot,endRot,timer);
                if (timer>=1f)
                {
                    break;
                }
                
                yield return waitFrame;
            }
            
            Destroy(gameObject);
        }

        #endregion
        
        #region Methods

        public void SetInactiveMaterialState(bool isInactive, Material inactiveMaterial = null) {
            if (isInactive == this.isInactive) {
                return; 
            }
            this.isInactive = isInactive;
            if (isInactive) {
                
                // Switch to Inactive Material
                meshRenderer.sharedMaterial = inactiveMaterial;
            } else {

                // Switch back to normal Material
                meshRenderer.sharedMaterial = material;
            }
        }

        #endregion
       
        
    }
}