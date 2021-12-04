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
            drawPileText.text = $"{CollectionManager.Instance.drawPile.Count.ToString()}";
            discardPileText.text = $"{CollectionManager.Instance.discardPile.Count.ToString()}";
            manaText.text = $"{GameManager.Instance.PersistentGameplayData.CurrentMana.ToString()}/{GameManager.Instance.PersistentGameplayData.MAXMana}";
        }

        public override void ResetCanvas()
        {
            base.ResetCanvas();
            combatWinPanel.SetActive(false);
            combatLosePanel.SetActive(false);
        }

        public void EndTurn()
        {
            if (CombatManager.Instance.CurrentCombatState == CombatState.AllyTurn)
            {
                CombatManager.Instance.EndTurn();
            }

        }
    }
}