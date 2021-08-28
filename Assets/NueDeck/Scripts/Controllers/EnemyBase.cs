using System;
using System.Collections;
using System.Collections.Generic;
using NueDeck.Scripts.Interfaces;
using NueDeck.Scripts.Managers;
using NueDeck.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace NueDeck.Scripts.Controllers
{
    [Serializable]
    public class EnemyAction
    {
        public enum EnemyActionType
        {
            Attack,
            Heal,
            Poison,
            Block,
            Space
        }
        //todo SO yap
        public EnemyActionType myEnemyActionType;
        public float value;
        public Sprite actionSprite;
        public SoundProfile mySoundProfile;
    }
    public class EnemyBase : MonoBehaviour,IEnemy
    {
        public List<EnemyAction> myActions;
        [HideInInspector] public Health myHealth;
        
        public Image actionImage;
        public TextMeshProUGUI nextActionText;
        public GameObject myCanvas;
        private EnemyAction _nextAction;
        public SoundProfile deathSoundProfile;
        public GameObject highlightObject;
        public Transform fxParent;

        private void Awake()
        {
            myHealth = GetComponent<Health>();
            myHealth.deathAction += OnDeath;
            highlightObject.SetActive(false);
        }
        
        public void ShowNextAction()
        {
            _nextAction = myActions[Random.Range(0, myActions.Count)];
            actionImage.sprite = _nextAction.actionSprite;
            if (_nextAction.value == -1)
            {
                nextActionText.gameObject.SetActive(false);
            }
            else
            {
                nextActionText.gameObject.SetActive(true);
                nextActionText.text = _nextAction.value.ToString();
            }
            

        }
        
        public void OnDeath()
        { 
            AudioManager.instance.PlayOneShot(deathSoundProfile.GetRandomClip());
            LevelManager.instance.OnEnemyDeath(this);
            Destroy(gameObject);
        }
        
        public IEnumerator ActionRoutine()
        {

            actionImage.gameObject.SetActive(false);
            
            switch (_nextAction.myEnemyActionType)
            {
                case EnemyAction.EnemyActionType.Attack:
                    yield return StartCoroutine(nameof(AttackAnim),_nextAction);
                    break;
                case EnemyAction.EnemyActionType.Heal:
                    yield return StartCoroutine(nameof(HealAnim),_nextAction);
                    break;
                case EnemyAction.EnemyActionType.Poison:
                    yield return StartCoroutine(nameof(PoisonAnim),_nextAction);
                    break;
                case EnemyAction.EnemyActionType.Block:
                    yield return StartCoroutine(nameof(BlockAnim),_nextAction);
                    break;
                case EnemyAction.EnemyActionType.Space:
                    yield return StartCoroutine(nameof(SpaceAnim),_nextAction);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            actionImage.gameObject.SetActive(true);
            
        }

        //todo Her enemy için ayrı sınıf aç
        #region EnemyActions

        private IEnumerator PoisonAnim(EnemyAction randomAction)
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = 0f;

            var startPos = transform.position;
            var endPos = LevelManager.instance.playerController.transform.position;

            var startRot = transform.localRotation;
            var endRot = Quaternion.Euler(60, 0, 60);
            
            while (true)
            {
                timer += Time.deltaTime*5;

                transform.position = Vector3.Lerp(startPos, endPos, timer);
                transform.localRotation = Quaternion.Lerp(startRot,endRot,timer);
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }

            timer = 0f;
            AudioManager.instance.PlayOneShot(randomAction.mySoundProfile.GetRandomClip());
            LevelManager.instance.playerController.myHealth.ApplyPoisonDamage(randomAction.value);
            FxManager.instance.PlayFx(LevelManager.instance.playerController.fxParent,FxManager.FxType.Poison);
            yield return new WaitForEndOfFrame();
            while (true)
            {
                timer += Time.deltaTime*5;

                transform.position = Vector3.Lerp(endPos, startPos, timer);
                transform.localRotation = Quaternion.Lerp(endRot,startRot,timer);
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }
        }
        
        private IEnumerator AttackAnim(EnemyAction randomAction)
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = 0f;

            var startPos = transform.position;
            var endPos = LevelManager.instance.playerController.transform.position;

            var startRot = transform.localRotation;
            var endRot = Quaternion.Euler(60, 0, 60);
            
            while (true)
            {
                timer += Time.deltaTime*5;

                transform.position = Vector3.Lerp(startPos, endPos, timer);
                transform.localRotation = Quaternion.Lerp(startRot,endRot,timer);
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }

            timer = 0f;
            AudioManager.instance.PlayOneShot(randomAction.mySoundProfile.GetRandomClip());
            LevelManager.instance.playerController.myHealth.TakeDamage(randomAction.value);
            FxManager.instance.PlayFx(LevelManager.instance.playerController.fxParent,FxManager.FxType.Attack);
            while (true)
            {
                timer += Time.deltaTime*5;

                transform.position = Vector3.Lerp(endPos, startPos, timer);
                transform.localRotation = Quaternion.Lerp(endRot,startRot,timer);
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }
        }

        private IEnumerator HealAnim(EnemyAction randomAction)
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = 0f;

            var startPos = transform.position;
            var endPos = startPos+new Vector3(0,0.2f,0);
            
            while (true)
            {
                timer += Time.deltaTime*5;

                transform.position = Vector3.Lerp(startPos, endPos, timer);
                
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }
            AudioManager.instance.PlayOneShot(randomAction.mySoundProfile.GetRandomClip());
            myHealth.Heal(randomAction.value);
            FxManager.instance.PlayFx(fxParent,FxManager.FxType.Heal);
            timer = 0f;
            while (true)
            {
                timer += Time.deltaTime*5;

                transform.position = Vector3.Lerp(endPos, startPos, timer);
                
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }
        }
        
        private IEnumerator BlockAnim(EnemyAction randomAction)
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = 0f;

            var startPos = transform.position;
            var endPos = startPos+new Vector3(0,0.2f,0);
            
            while (true)
            {
                timer += Time.deltaTime*5;

                transform.position = Vector3.Lerp(startPos, endPos, timer);
                
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }
            AudioManager.instance.PlayOneShot(randomAction.mySoundProfile.GetRandomClip());
            myHealth.ApplyBlock(randomAction.value);
            FxManager.instance.PlayFx(fxParent,FxManager.FxType.Block);
            timer = 0f;
            while (true)
            {
                timer += Time.deltaTime*5;

                transform.position = Vector3.Lerp(endPos, startPos, timer);
                
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }
        }
        
        private IEnumerator SpaceAnim(EnemyAction randomAction)
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = 0f;

            var startPos = transform.position;
            var endPos = startPos+new Vector3(0,0.2f,0);
            
            while (true)
            {
                timer += Time.deltaTime*5;

                transform.position = Vector3.Lerp(startPos, endPos, timer);
                
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }
            AudioManager.instance.PlayOneShot(randomAction.mySoundProfile.GetRandomClip());
            HandManager.instance.ExhaustRandomCard();
            FxManager.instance.PlayFx(fxParent,FxManager.FxType.Buff);
            FxManager.instance.PlayFx(LevelManager.instance.playerController.fxParent,FxManager.FxType.Attack);
            timer = 0f;
            while (true)
            {
                timer += Time.deltaTime*5;

                transform.position = Vector3.Lerp(endPos, startPos, timer);
                
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }
        }

        #endregion


        public void OnCardTargetHighlight()
        {
            throw new NotImplementedException();
        }

        public void OnCardOverHighlight()
        {
            throw new NotImplementedException();
        }

        public void OnCardPlayedForMe()
        {
            throw new NotImplementedException();
        }

        public EnemyBase GetEnemyBase()
        {
            return this;
        }
    }
}