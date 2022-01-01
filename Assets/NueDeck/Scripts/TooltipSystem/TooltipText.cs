using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NueDeck.Scripts.TooltipSystem
{
    public class TooltipText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private TextMeshProUGUI contentText;
        [SerializeField] private LayoutElement layoutElement;
        [SerializeField] private int characterWrapLimit = 50;


        public void SetText(string content,string header ="")
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

            contentText.text = content;
            
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