using System.Collections;
using NueDeck.Scripts.Characters.Enemies;
using NueDeck.Scripts.Data;
using NueDeck.Scripts.Data.Characters;
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
        public EnemyCanvas enemyCanvas;
      
        protected EnemyAbilityData NextAbility;
        
        #region Setup

        public override void BuildCharacter()
        {
            base.BuildCharacter();
            enemyCanvas.InitCanvas();
            CharacterStats = new CharacterStats(enemyData.maxHealth,enemyCanvas);
            CharacterStats.OnDeath += OnDeath;
            CharacterStats.SetCurrentHealth(CharacterStats.CurrentHealth);
            CombatManager.instance.OnAllyTurnStarted += ShowNextAbility;
            CombatManager.instance.OnEnemyTurnStarted += CharacterStats.TriggerAllStatus;
        }

        protected override void OnDeath()
        {
            base.OnDeath();
            CombatManager.instance.OnAllyTurnStarted -= ShowNextAbility;
            CombatManager.instance.OnEnemyTurnStarted -= CharacterStats.TriggerAllStatus;
            CombatManager.instance.OnEnemyDeath(this);
            Destroy(gameObject);
        }

        #endregion

        #region Other Methods

        public void ShowNextAbility()
        {
            NextAbility = enemyData.enemyAbilityList.RandomItem();
            enemyCanvas.intentionImage.sprite = NextAbility.intention.intentionSprite;
            
            if (NextAbility.hideActionValue)
            {
                enemyCanvas.nextActionValueText.gameObject.SetActive(false);
            }
            else
            {
                enemyCanvas.nextActionValueText.gameObject.SetActive(true);
                enemyCanvas.nextActionValueText.text = NextAbility.actionList[0].value.ToString();
            }
            
            enemyCanvas.intentionImage.gameObject.SetActive(true);
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

        public CharacterBase GetCharacterBase() => this;
        
        #endregion
        
        #region Action Routines

        public virtual IEnumerator ActionRoutine()
        {
            enemyCanvas.intentionImage.gameObject.SetActive(false);
            if (NextAbility.intention.enemyIntention == EnemyIntentions.Attack || NextAbility.intention.enemyIntention == EnemyIntentions.Debuff)
            {
                yield return StartCoroutine(AttackRoutine(NextAbility));
            }
            else
            {
                yield return StartCoroutine(BuffRoutine(NextAbility));
            }
            
           
        }
        
        protected virtual IEnumerator AttackRoutine(EnemyAbilityData targetAbility)
        {
            var waitFrame = new WaitForEndOfFrame();

            var target = CombatManager.instance.currentAllies.RandomItem();
            
            var startPos = transform.position;
            var endPos = target.transform.position;

            var startRot = transform.localRotation;
            var endRot = Quaternion.Euler(60, 0, 60);
            
            
            yield return StartCoroutine(MoveToTargetRoutine(waitFrame, startPos, endPos, startRot, endRot, 5));
          
            targetAbility.actionList.ForEach(x=>EnemyActionProcessor.GetAction(x.enemyActionType).DoAction(new EnemyActionParameters(x.value,target,this)));
            
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
            
            targetAbility.actionList.ForEach(x=>EnemyActionProcessor.GetAction(x.enemyActionType).DoAction(new EnemyActionParameters(x.value,null,this)));
            
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