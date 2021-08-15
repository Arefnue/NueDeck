using NueDeck.Scripts.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NueDeck.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        public TextMeshProUGUI drawPileText;
        public TextMeshProUGUI discardPileText;
        public TextMeshProUGUI manaText;
        public GameObject gameCanvas;
        public GameObject winPanel;
        public GameObject losePanel;

        public TextMeshProUGUI healthText;
        public TextMeshProUGUI goldText;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI roomText;
        public TextMeshProUGUI malfunctionNameText;
        public TextMeshProUGUI malfunctionCounterText;

        public GameObject randomizedDeck;
        private void Awake()
        {
            instance = this;
            winPanel.SetActive(false);
            losePanel.SetActive(false);
            randomizedDeck.SetActive(GameManager.instance.isRandomHand);
        }


        private void Start()
        {
            UpdateAllNotificationText();
        }

        public void SetPileTexts()
        {
            drawPileText.text = $"{HandManager.instance.drawPile.Count.ToString()}";
            discardPileText.text = $"{HandManager.instance.discardPile.Count.ToString()}";
            manaText.text = $"{HandManager.instance.currentMana.ToString()}/{GameManager.instance.maxMana}";
        }

        public void UpdateAllNotificationText()
        {
            UpdateHealthText();
            UpdateGoldText();
            UpdateNameText();
            UpdateRoomText();
            
        }

        public void UpdateRoomText()
        {
            roomText.text = $"Room {GameManager.instance.GetCurrentLevel().ToString()}";
        }

        public void UpdateNameText()
        {
            nameText.text = $"{GameManager.instance.playerName}";
        }

        public void UpdateGoldText()
        {
            goldText.text = $"{GameManager.instance.currentGold.ToString()}";
        }

        public void UpdateHealthText()
        {
            healthText.text = $"{LevelManager.instance.playerController.myHealth.maxHealth.ToString()}/{GameManager.instance.playerMaxHealth.ToString()}";
        }
      
        public void EndTurn()
        {
            if (LevelManager.instance.CurrentLevelState == LevelManager.LevelState.PlayerTurn)
            {
                LevelManager.instance.EndTurn();
            }

        }

        public void MainMenu()
        {
            GameManager.instance.ResetManager();
            SceneManager.LoadScene(1);
        }
    }
}
