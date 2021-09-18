using System;
using System.Collections;
using System.Collections.Generic;
using NueDeck.Scripts.Collection;
using NueDeck.Scripts.EnemyBehaviour;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Interfaces;
using NueDeck.Scripts.Managers;
using NueDeck.Scripts.Utils;
using NueExtentions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace NueDeck.Scripts.Characters.Enemies
{
    public class EnemyExample : EnemyBase
    {
        [Header("References")]
        public Image intentionImage;
        public TextMeshProUGUI nextActionValueText;
        public GameObject myCanvas;
        public EnemyData enemyData;
        public SoundProfileData deathSoundProfileData;
        public GameObject highlightObject;
        public Transform fxParent;

        private EnemyAbilityData _nextAbility;
        
        private void Awake()
        {
            highlightObject.SetActive(false);
        }
        
        public void ShowNextAction()
        {
            _nextAbility = enemyData.enemyAbilityList.RandomItem();
            intentionImage.sprite = _nextAbility.intention.intentionSprite;
            
            if (_nextAbility.hideActionValue)
            {
                nextActionValueText.gameObject.SetActive(false);
            }
            else
            {
                nextActionValueText.gameObject.SetActive(true);
                nextActionValueText.text = _nextAbility.actionList[0].value.ToString();
            }
            

        }
        
        public void OnDeath()
        { 
            AudioManager.instance.PlayOneShot(deathSoundProfileData.GetRandomClip());
            Destroy(gameObject);
        }
        
        public IEnumerator ActionRoutine()
        {

            intentionImage.gameObject.SetActive(false);

            if (_nextAbility.intention.enemyIntention == EnemyIntentions.Attack || _nextAbility.intention.enemyIntention == EnemyIntentions.Debuff)
            {
                yield return StartCoroutine(AttackRoutine(_nextAbility));
            }
            else
            {
                yield return StartCoroutine(BuffRoutine(_nextAbility));
            }
            
            intentionImage.gameObject.SetActive(true);
            
        }
        
        #region EnemyActions

        private IEnumerator MoveToTargetRoutine(WaitForEndOfFrame waitFrame,Vector3 startPos, Vector3 endPos, Quaternion startRot, Quaternion endRot, float speed)
        {
            var timer = 0f;
            while (true)
            {
                timer += Time.deltaTime*speed;

                transform.position = Vector3.Lerp(startPos, endPos, timer);
                transform.localRotation = Quaternion.Lerp(startRot,endRot,timer);
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }
        }

        private IEnumerator AttackRoutine(EnemyAbilityData targetAbility)
        {
            var waitFrame = new WaitForEndOfFrame();

            var target = LevelManager.instance.playerExample;
            
            var startPos = transform.position;
            var endPos = target.transform.position;

            var startRot = transform.localRotation;
            var endRot = Quaternion.Euler(60, 0, 60);
            
            
            yield return StartCoroutine(MoveToTargetRoutine(waitFrame, startPos, endPos, startRot, endRot, 5));
          
            targetAbility.actionList.ForEach(x=>EnemyActionProcessor.GetAction(x.enemyActionType).DoAction(new EnemyActionParameters(x.value,target)));
            
            yield return StartCoroutine(MoveToTargetRoutine(waitFrame, endPos, startPos, endRot, startRot, 5));
            
        }
        
        private IEnumerator BuffRoutine(EnemyAbilityData targetAbility)
        {
            var waitFrame = new WaitForEndOfFrame();
            
            var startPos = transform.position;
            var endPos = startPos+new Vector3(0,0.2f,0);
            
            var startRot = transform.localRotation;
            var endRot = transform.localRotation;
            
            yield return StartCoroutine(MoveToTargetRoutine(waitFrame, startPos, endPos, startRot, endRot, 5));
            
            targetAbility.actionList.ForEach(x=>EnemyActionProcessor.GetAction(x.enemyActionType).DoAction(new EnemyActionParameters(x.value,null)));
            
            yield return StartCoroutine(MoveToTargetRoutine(waitFrame, endPos, startPos, endRot, startRot, 5));
        }
        
        #endregion
        
    }
}