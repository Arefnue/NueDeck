using System;
using System.Collections.Generic;
using System.Linq;
using NueDeck.Scripts.Data.Containers;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Managers;
using NueDeck.Scripts.UI;
using NueTooltip.Core;
using NueTooltip.CursorSystem;
using NueTooltip.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NueDeck.Scripts.Characters
{
    [RequireComponent(typeof(Canvas))]
    public abstract class CharacterCanvas : MonoBehaviour,I2DTooltipTarget
    {
        [Header("References")]
        [SerializeField] protected Transform statusIconRoot;
        [SerializeField] protected Transform highlightRoot;
        [SerializeField] protected Transform descriptionRoot;
        [SerializeField] protected StatusIconsData statusIconsData;
        [SerializeField] protected TextMeshProUGUI currentHealthText;
        [SerializeField] protected Image highlightImage;
        
        protected Dictionary<StatusType, StatusIconBase> _statusDict = new Dictionary<StatusType, StatusIconBase>();

        protected Canvas _canvas;

        #region Setup

        public void InitCanvas()
        {
            highlightRoot.gameObject.SetActive(false);
            
            for (int i = 0; i < Enum.GetNames(typeof(StatusType)).Length; i++)
                _statusDict.Add((StatusType) i, null);

            _canvas = GetComponent<Canvas>();

            if (_canvas)
                _canvas.worldCamera = Camera.main;
        }

        #endregion
        
        #region Public Methods
        public void ApplyStatus(StatusType targetStatus, int value)
        {
            if (_statusDict[targetStatus] == null)
            {
                var targetData = statusIconsData.StatusIconList.FirstOrDefault(x => x.IconStatus == targetStatus);
                
                if (targetData == null) return;
                
                var clone = Instantiate(statusIconsData.StatusIconBasePrefab, statusIconRoot);
                clone.SetStatus(targetData);
                _statusDict[targetStatus] = clone;
            }
            
            _statusDict[targetStatus].SetStatusValue(value);
        }

        public void ClearStatus(StatusType targetStatus)
        {
            if (_statusDict[targetStatus])
            {
                Destroy(_statusDict[targetStatus].gameObject);
            }
           
            _statusDict[targetStatus] = null;
        }
        
        public void UpdateStatusText(StatusType targetStatus, int value)
        {
            if (_statusDict[targetStatus] == null) return;
          
            _statusDict[targetStatus].StatusValueText.text = $"{value}";
        }
        
        public void UpdateHealthText(int currentHealth,int maxHealth) =>  currentHealthText.text = $"{currentHealth}/{maxHealth}";
        public void SetHighlight(bool open) => highlightRoot.gameObject.SetActive(open);
       
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

        #endregion

        #region Tooltip
        public void ShowTooltipInfo()
        {
            var tooltipManager = TooltipManager.Instance;
            var specialKeywords = new List<SpecialKeywords>();
            
            foreach (var statusIcon in _statusDict)
            {
                if (statusIcon.Value == null) continue;
               
                var statusData = statusIcon.Value.MyStatusIconData;
                foreach (var statusDataSpecialKeyword in statusData.SpecialKeywords)
                {
                    if (specialKeywords.Contains(statusDataSpecialKeyword)) continue;
                    specialKeywords.Add(statusDataSpecialKeyword);
                }
            }
            
            foreach (var specialKeyword in specialKeywords)
            {
                var specialKeywordData =tooltipManager.SpecialKeywordData.SpecialKeywordBaseList.Find(x => x.SpecialKeyword == specialKeyword);
                if (specialKeywordData != null)
                    ShowTooltipInfo(tooltipManager,specialKeywordData.GetContent(),specialKeywordData.GetHeader(),descriptionRoot);
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