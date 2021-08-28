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
        public GameObject randomizedDeck;
        
        private void Awake()
        {
            instance = this;
            randomizedDeck.SetActive(HandManager.instance.isRandomHand);
        }
        
        public void SetPileTexts()
        {
            drawPileText.text = $"{HandManager.instance.drawPile.Count.ToString()}";
            discardPileText.text = $"{HandManager.instance.discardPile.Count.ToString()}";
            manaText.text = $"{HandManager.instance.currentMana.ToString()}/{HandManager.instance.maxMana}";
        }

        
        public void EndTurn()
        {
            if (LevelManager.instance.CurrentLevelState == LevelManager.LevelState.PlayerTurn)
            {
                LevelManager.instance.EndTurn();
            }

        }
        
    }
}
