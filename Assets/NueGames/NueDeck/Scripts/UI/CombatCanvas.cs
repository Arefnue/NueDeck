using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.UI
{
    public class CombatCanvas : CanvasBase
    {
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI drawPileTextField;
        [SerializeField] private TextMeshProUGUI discardPileTextField;
        [SerializeField] private TextMeshProUGUI manaTextTextField;
        
        [Header("Panels")]
        [SerializeField] private GameObject combatWinPanel;
        [SerializeField] private GameObject combatLosePanel;

        public TextMeshProUGUI DrawPileTextField => drawPileTextField;
        public TextMeshProUGUI DiscardPileTextField => discardPileTextField;
        public TextMeshProUGUI ManaTextTextField => manaTextTextField;
        public GameObject CombatWinPanel => combatWinPanel;
        public GameObject CombatLosePanel => combatLosePanel;

        #region Setup
        private void Awake()
        {
            CombatWinPanel.SetActive(false);
            CombatLosePanel.SetActive(false);
        }
        #endregion

        #region Public Methods
        public void SetPileTexts()
        {
            DrawPileTextField.text = $"{CollectionManager.Instance.DrawPile.Count.ToString()}";
            DiscardPileTextField.text = $"{CollectionManager.Instance.DiscardPile.Count.ToString()}";
            ManaTextTextField.text = $"{GameManager.Instance.PersistentGameplayData.CurrentMana.ToString()}/{GameManager.Instance.PersistentGameplayData.MaxMana}";
        }

        public override void ResetCanvas()
        {
            base.ResetCanvas();
            CombatWinPanel.SetActive(false);
            CombatLosePanel.SetActive(false);
        }

        public void EndTurn()
        {
            if (CombatManager.Instance.CurrentCombatStateType == CombatStateType.AllyTurn)
                CombatManager.Instance.EndTurn();
        }
        #endregion
    }
}