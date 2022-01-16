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
    public abstract class CharacterCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentHealthText;
        [SerializeField] private Transform statusIconRoot;
        [SerializeField] private StatusIconsData statusIconsData;
        [SerializeField] private Transform highlightRoot;
        [SerializeField] private Transform descriptionRoot;
        
        
        private Dictionary<StatusType, StatusIcon> _statusDict = new Dictionary<StatusType, StatusIcon>();

        private Canvas _canvas;
        
        private void Awake()
        {
            highlightRoot.gameObject.SetActive(false);
        }

        public void InitCanvas()
        {
            for (int i = 0; i < Enum.GetNames(typeof(StatusType)).Length; i++)
            {
                _statusDict.Add((StatusType) i, null);
            }

            _canvas = GetComponent<Canvas>();
            if (_canvas)
                _canvas.worldCamera = GameManager.Instance.mainCam;
        }

        public void ApplyStatus(StatusType targetStatus, int value)
        {
            if (_statusDict[targetStatus] == null)
            {
                var targetData = statusIconsData.statusIconList.FirstOrDefault(x => x.iconStatus == targetStatus);
                
                if (targetData == null) return;
                
                var clone = Instantiate(statusIconsData.statusIconPrefab, statusIconRoot);
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
            if (_statusDict[targetStatus] == null)  return;
          
            _statusDict[targetStatus].statusValueText.text = $"{value}";
        }
        
        public void UpdateHealthText(int currentHealth,int maxHealth)
        {
            currentHealthText.text = $"{currentHealth}/{maxHealth}";
        }

        public void SetHighlight(bool open)
        {
            highlightRoot.gameObject.SetActive(open);
        }
        
    }
}