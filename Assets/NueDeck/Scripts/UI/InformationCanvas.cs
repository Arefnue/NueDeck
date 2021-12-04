using System;
using NueDeck.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace NueDeck.Scripts.UI
{
    public class InformationCanvas : CanvasBase
    {
        public GameObject randomizedDeck;
        public TextMeshProUGUI roomText;
        public TextMeshProUGUI goldText;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI healthText;
      
        private void Awake()
        {
           ResetCanvas();
        }

        public void SetRoomText(int roomNumber,bool useStage = false, int stageNumber = -1) => 
            roomText.text = useStage ? $"Room {stageNumber}/{roomNumber}" : $"Room {roomNumber}";

        public void SetGoldText(int value)=>goldText.text = $"{value}";

        public void SetNameText(string name) => nameText.text = $"{name}";

        public void SetHealthText(int currentHealth,int maxHealth) => healthText.text = $"{currentHealth}/{maxHealth}";

        public override void ResetCanvas()
        {
            randomizedDeck.SetActive(GameManager.Instance.PersistentGameplayData.IsRandomHand);
            SetHealthText(GameManager.Instance.PersistentGameplayData.AllyList[0].allyData.maxHealth,GameManager.Instance.PersistentGameplayData.AllyList[0].allyData.maxHealth);
            SetNameText(GameManager.Instance.GameplayData.defaultName);
            SetRoomText(GameManager.Instance.PersistentGameplayData.CurrentEncounterId+1,GameManager.Instance.GameplayData.useStageSystem,GameManager.Instance.PersistentGameplayData.CurrentStageId+1);
        }
    }
}