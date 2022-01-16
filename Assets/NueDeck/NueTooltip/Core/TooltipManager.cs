using System.Collections;
using System.Collections.Generic;
using NueDeck.Scripts.Data.Containers;
using NueTooltip.CursorSystem;
using UnityEngine;

namespace NueTooltip.Core
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance;
        
        [Header("References")]
        [SerializeField] private TooltipController tooltipController;
        [SerializeField] private CursorController cursorController;
        [SerializeField] private TooltipText tooltipTextPrefab;
        [SerializeField] private CanvasGroup canvasGroup;
       
        [Header("Settings")]
        [SerializeField] private AnimationCurve fadeCurve;
        [SerializeField] private float showDelayTime = 0.5f;
        [SerializeField] private bool canChangeCursor;
        
     
        private List<TooltipText> _tooltipTextList = new List<TooltipText>();
        private TooltipController TooltipController => tooltipController;
        private CursorController CursorController => cursorController;

        private int _currentShownTooltipCount;
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }

        private IEnumerator ShowRoutine()
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = 0f;
            canvasGroup.alpha = 0;
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
        public void ShowTooltip(string contentText="",string headerText ="",Transform tooltipTargetTransform = null,CursorType cursorType = CursorType.Default)
        {
            StartCoroutine(nameof(ShowRoutine));
            _currentShownTooltipCount++;
            if (_tooltipTextList.Count<_currentShownTooltipCount)
            {
                var newTooltip = Instantiate(tooltipTextPrefab, TooltipController.transform);
                _tooltipTextList.Add(newTooltip);
            }
            
            _tooltipTextList[_currentShownTooltipCount-1].gameObject.SetActive(true);
            _tooltipTextList[_currentShownTooltipCount-1].SetText(contentText,headerText);
            
            TooltipController.SetFollowPos(tooltipTargetTransform);
            
            if (canChangeCursor)
                CursorController.SetActiveCursor(cursorType);
            
        }

        public void HideTooltip()
        {
            StopCoroutine(nameof(ShowRoutine));
            
            _currentShownTooltipCount = 0;
            
            foreach (var tooltipText in _tooltipTextList)
                tooltipText.gameObject.SetActive(false);

            if (canChangeCursor)
                CursorController.SetActiveCursor(CursorType.Default);
           
        }
        
    }
}