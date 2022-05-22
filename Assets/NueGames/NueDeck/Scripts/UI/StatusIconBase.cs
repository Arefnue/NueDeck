using NueGames.NueDeck.Scripts.Data.Containers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NueGames.NueDeck.Scripts.UI
{
    public class StatusIconBase : MonoBehaviour
    {
        [SerializeField] private Image statusImage;
        [SerializeField] private TextMeshProUGUI statusValueText;

        public StatusIconData MyStatusIconData { get; private set; } = null;

        public Image StatusImage => statusImage;

        public TextMeshProUGUI StatusValueText => statusValueText;

        public void SetStatus(StatusIconData statusIconData)
        {
            MyStatusIconData = statusIconData;
            StatusImage.sprite = statusIconData.IconSprite;
            
        }

        public void SetStatusValue(int statusValue)
        {
            StatusValueText.text = statusValue.ToString();
        }
    }
}