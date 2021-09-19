using NueDeck.Scripts.Collection;
using NueDeck.Scripts.Enums;
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
            randomizedDeck.SetActive(CollectionManager.instance.isRandomHand);
        }
        
        public void SetPileTexts()
        {
            drawPileText.text = $"{CollectionManager.instance.drawPile.Count.ToString()}";
            discardPileText.text = $"{CollectionManager.instance.discardPile.Count.ToString()}";
            manaText.text = $"{CollectionManager.instance.currentMana.ToString()}/{CollectionManager.instance.maxMana}";
        }

        
        public void EndTurn()
        {
            if (CombatManager.instance.CurrentCombatState == CombatState.AllyTurn)
            {
                CombatManager.instance.EndTurn();
            }

        }
        
    }
}
