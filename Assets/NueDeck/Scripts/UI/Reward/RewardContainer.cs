using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NueDeck.Scripts.UI.Reward
{
    public class RewardContainer : MonoBehaviour
    {

        public Button rewardButton;
        [SerializeField] private Image rewardImage;
        [SerializeField] private TextMeshProUGUI rewardText;

        public void BuildReward(Sprite rewardSprite,string rewardDescription)
        {
            rewardImage.sprite = rewardSprite;
            rewardText.text = rewardDescription;
        }
        
    }
}