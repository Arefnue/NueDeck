using System.Collections;
using System.Collections.Generic;
using NueGames.NueDeck.Scripts.Data.Containers;
using NueGames.NueDeck.ThirdParty.NueTooltip.CursorSystem;
using UnityEngine;

namespace NueGames.NueDeck.ThirdParty.NueTooltip.Core
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance;
        
        [Header("References")]
        [SerializeField] private TooltipController tooltipController;
        [SerializeField] private CursorController cursorController;
        [SerializeField] private TooltipText tooltipTextPrefab;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private SpecialKeywordData specialKeywordData;
        
        [Header("Settings")]
        [SerializeField] private AnimationCurve fadeCurve;
        [SerializeField] private float showDelayTime = 0.5f;
        [SerializeField] private bool canChangeCursor;

        public SpecialKeywordData SpecialKeywordData => specialKeywordData;
     
        private List<TooltipText> _tooltipTextList = new List<TooltipText>();
        private TooltipController TooltipController => tooltipController;
        private CursorController CursorController => cursorController;

        private int _currentShownTooltipCount;
        private void Awake()
        {
            if (Instance == null)
            {
                transform.parent = null;
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            
        }

        private IEnumerator ShowRoutine(float delay = 0)
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = 0f;
            
            
            canvasGroup.alpha = 0;

            yield return new WaitForSeconds(delay);
            
            while (true)
            {
                timer += Time.deltaTime;

                var invValue = Mathf.InverseLerp(0, showDelayTime, timer);
                canvasGroup.alpha = fadeCurve.Evaluate(invValue);
                
                if (timer>=showDelayTime)
                { 
                    canvasGroup.alpha = 1;
                    break;
                }
                yield return waitFrame;
            }
        }
        public void ShowTooltip(string contentText="",string headerText ="",Transform tooltipTargetTransform = null,CursorType cursorType = CursorType.Default, Camera cam = null, float delayShow =0)
        {
            StartCoroutine(ShowRoutine(delayShow));
            _currentShownTooltipCount++;
            if (_tooltipTextList.Count<_currentShownTooltipCount)
            {
                var newTooltip = Instantiate(tooltipTextPrefab, TooltipController.transform);
                _tooltipTextList.Add(newTooltip);
            }
            
            _tooltipTextList[_currentShownTooltipCount-1].gameObject.SetActive(true);
            _tooltipTextList[_currentShownTooltipCount-1].SetText(contentText,headerText);
            
            TooltipController.SetFollowPos(tooltipTargetTransform,cam);
            
            if (canChangeCursor)
                CursorController.SetActiveCursor(cursorType);
            
        }

        public void HideTooltip()
        {
            StopAllCoroutines();
            _currentShownTooltipCount = 0;
            canvasGroup.alpha = 0;
            foreach (var tooltipText in _tooltipTextList)
                tooltipText.gameObject.SetActive(false);

            if (canChangeCursor)
                CursorController.SetActiveCursor(CursorType.Default);
           
        }
        
    }
}