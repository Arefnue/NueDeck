using UnityEngine;

namespace NueDeck.Scripts.TooltipSystem
{
    public abstract class TooltipTriggerBase : MonoBehaviour, ITooltipTargetBase
    {
        
        [SerializeField] protected string headerText = "";
        [TextArea] [SerializeField] protected string contentText;
        [SerializeField] private Transform tooltipStaticTargetTransform;


        protected virtual void ShowTooltipInfo()
        {
            ShowTooltipInfo(TooltipManager.Instance,contentText,headerText,tooltipStaticTargetTransform);
        }

        public void ShowTooltipInfo(TooltipManager tooltipManager, string content, string header = "", Transform tooltipStaticTransform = null)
        {
            tooltipManager.ShowTooltip(content,header,tooltipStaticTransform);
        }

        public virtual void HideTooltipInfo()
        {
            TooltipManager.Instance.HideTooltip();
        }
    }
}