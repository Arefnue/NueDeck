using System;
using NueDeck.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace NueDeck.Scripts.UI
{
    public class InformationCanvas : CanvasBase
    {
        [Header("Settings")] 
        [SerializeField] private GameObject randomizedDeckObject;
        [SerializeField] private TextMeshProUGUI roomTextField;
        [SerializeField] private TextMeshProUGUI goldTextField;
        [SerializeField] private TextMeshProUGUI nameTextField;
        [SerializeField] private TextMeshProUGUI healthTextField;

        public GameObject RandomizedDeckObject => randomizedDeckObject;
        public TextMeshProUGUI RoomTextField => roomTextField;
        public TextMeshProUGUI GoldTextField => goldTextField;
        public TextMeshProUGUI NameTextField => nameTextField;
        public TextMeshProUGUI HealthTextField => healthTextField;
        
        #region Setup
        private void Awake()
        {
            ResetCanvas();
        }
        #endregion
        
        #region Public Methods
        public void SetRoomText(int roomNumber,bool useStage = false, int stageNumber = -1) => 
            RoomTextField.text = useStage ? $"Room {stageNumber}/{roomNumber}" : $"Room {roomNumber}";

        public void SetGoldText(int value)=>GoldTextField.text = $"{value}";

        public void SetNameText(string name) => NameTextField.text = $"{name}";

        public void SetHealthText(int currentHealth,int maxHealth) => HealthTextField.text = $"{currentHealth}/{maxHealth}";

        public override void ResetCanvas()
        {
            RandomizedDeckObject.SetActive(GameManager.Instance.PersistentGameplayData.IsRandomHand);
            SetHealthText(GameManager.Instance.PersistentGameplayData.AllyList[0].AllyData.maxHealth,GameManager.Instance.PersistentGameplayData.AllyList[0].AllyData.maxHealth);
            SetNameText(GameManager.Instance.GameplayData.defaultName);
            SetRoomText(GameManager.Instance.PersistentGameplayData.CurrentEncounterId+1,GameManager.Instance.GameplayData.useStageSystem,GameManager.Instance.PersistentGameplayData.CurrentStageId+1);
        }
        #endregion
        
    }
}