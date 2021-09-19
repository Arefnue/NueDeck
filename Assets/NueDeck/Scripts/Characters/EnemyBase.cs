using System.Collections;
using NueDeck.Scripts.Characters.Enemies;
using NueDeck.Scripts.EnemyBehaviour;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Interfaces;
using NueDeck.Scripts.Managers;
using NueExtentions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NueDeck.Scripts.Characters
{
    public class EnemyBase : CharacterBase, IEnemy
    {
        [Header("Enemy Base References")]
        public EnemyData enemyData;
        public Image intentionImage;
        protected EnemyAbilityData nextAbility;
        public TextMeshProUGUI nextActionValueText;
        
        #region Setup

        protected override void OnDeath()
        {
            base.OnDeath();
        }

        #endregion

        #region Other Methods

        public void ShowNextAbility()
        {
            nextAbility = enemyData.enemyAbilityList.RandomItem();
            intentionImage.sprite = nextAbility.intention.intentionSprite;
            
            if (nextAbility.hideActionValue)
            {
                nextActionValueText.gameObject.SetActive(false);
            }
            else
            {
                nextActionValueText.gameObject.SetActive(true);
                nextActionValueText.text = nextAbility.actionList[0].value.ToString();
            }
        }

        #endregion
        
        #region Interface Methods

        public void OnCardTargetHighlight()
        {
            
        }

        public void OnCardOverHighlight()
        {
            
        }

        public void OnCardPlayedForMe()
        {
            
        }

        public EnemyBase GetEnemyBase() => this;

        #endregion
        
        #region Action Routines

        public virtual IEnumerator ActionRoutine()
        {
            intentionImage.gameObject.SetActive(false);
            if (nextAbility.intention.enemyIntention == EnemyIntentions.Attack || nextAbility.intention.enemyIntention == EnemyIntentions.Debuff)
            {
                yield return StartCoroutine(AttackRoutine(nextAbility));
            }
            else
            {
                yield return StartCoroutine(BuffRoutine(nextAbility));
            }
            
            intentionImage.gameObject.SetActive(true);
        }
        
        protected virtual IEnumerator AttackRoutine(EnemyAbilityData targetAbility)
        {
            var waitFrame = new WaitForEndOfFrame();

            var target = LevelManager.instance.currentAllies.RandomItem();
            
            var startPos = transform.position;
            var endPos = target.transform.position;

            var startRot = transform.localRotation;
            var endRot = Quaternion.Euler(60, 0, 60);
            
            
            yield return StartCoroutine(MoveToTargetRoutine(waitFrame, startPos, endPos, startRot, endRot, 5));
          
            targetAbility.actionList.ForEach(x=>EnemyActionProcessor.GetAction(x.enemyActionType).DoAction(new EnemyActionParameters(x.value,target)));
            
            yield return StartCoroutine(MoveToTargetRoutine(waitFrame, endPos, startPos, endRot, startRot, 5));
            
        }
        
        protected virtual IEnumerator BuffRoutine(EnemyAbilityData targetAbility)
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
        
        #region Other Routines

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

        #endregion
        
       
        
    }
}