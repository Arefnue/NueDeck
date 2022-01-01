using System.Collections.Generic;
using UnityEngine;

namespace NueDeck.Scripts.TooltipSystem
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance;
        
        [SerializeField] private TooltipController tooltipController;
        public TooltipController TooltipController => tooltipController;

        [SerializeField] private TooltipText tooltipTextPrefab;

        private List<TooltipText> _tooltipTextList = new List<TooltipText>();

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

        public void ShowTooltip(string contentText,string headerText ="",Transform tooltipTargetTransform = null)
        {
            _currentShownTooltipCount++;
            if (_tooltipTextList.Count<_currentShownTooltipCount)
            {
                var newTooltip = Instantiate(tooltipTextPrefab, TooltipController.transform);
                _tooltipTextList.Add(newTooltip);
            }
            
            _tooltipTextList[_currentShownTooltipCount-1].gameObject.SetActive(true);
            _tooltipTextList[_currentShownTooltipCount-1].SetText(contentText,headerText);
            
            TooltipController.SetFollowPos(tooltipTargetTransform);
        }

        public void HideTooltip()
        {
            _currentShownTooltipCount = 0;
            foreach (var tooltipText in _tooltipTextList)
                tooltipText.gameObject.SetActive(false);
        }
        
    }
}