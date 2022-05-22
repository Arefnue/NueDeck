using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NueGames.NueDeck.ThirdParty.NueTooltip.Core
{
    public class TooltipText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private TextMeshProUGUI contentText;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private LayoutElement layoutElement;
        [SerializeField] private int characterWrapLimit = 50;


        public void SetText(string content="",string header ="")
        {
            if (string.IsNullOrEmpty(header))
            {
                headerText.gameObject.SetActive(false);
            }
            else
            {
                headerText.gameObject.SetActive(true);
                headerText.text = header;
            }

            if (string.IsNullOrEmpty(content))
            {
                contentText.gameObject.SetActive(false);
            }
            else
            {
                contentText.gameObject.SetActive(true);
                contentText.text = content;
            }

            if (contentText.gameObject.activeSelf || headerText.gameObject.activeSelf)
            {
                backgroundImage.gameObject.SetActive(true);
            }
            else
            {
                backgroundImage.gameObject.SetActive(false);
            }
            
            PrepareLayout();
        }


        private void PrepareLayout()
        {
            var longestTextLength = GetLongestTextLength();
            layoutElement.enabled = (longestTextLength > characterWrapLimit);
        }

        private int GetLongestTextLength()
        {
            if (headerText.text.Length > contentText.text.Length)
                return headerText.text.Length;

            if (headerText.text.Length <= contentText.text.Length)
                return contentText.text.Length;

            return 0;
        }
    }
}