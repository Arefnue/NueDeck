using System;
using NueDeck.Scripts.Collection;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Interfaces;
using NueDeck.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace NueDeck.Scripts.UI
{
    public class CombatCanvas : CanvasBase
    {
        public TextMeshProUGUI drawPileText;
        public TextMeshProUGUI discardPileText;
        public TextMeshProUGUI manaText;
        public GameObject combatWinPanel;
        public GameObject combatLosePanel;

        private void Awake()
        {
            combatWinPanel.SetActive(false);
            combatLosePanel.SetActive(false);
        }
        
        public void SetPileTexts()
        {
            drawPileText.text = $"{CollectionManager.instance.drawPile.Count.ToString()}";
            discardPileText.text = $"{CollectionManager.instance.discardPile.Count.ToString()}";
            manaText.text = $"{GameManager.instance.PersistentGameplayData.CurrentMana.ToString()}/{GameManager.instance.PersistentGameplayData.MAXMana}";
        }

        public override void ResetCanvas()
        {
            base.ResetCanvas();
            combatWinPanel.SetActive(false);
            combatLosePanel.SetActive(false);
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