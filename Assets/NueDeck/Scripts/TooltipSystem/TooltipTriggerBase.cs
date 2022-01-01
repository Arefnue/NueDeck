using UnityEngine;

namespace NueDeck.Scripts.TooltipSystem
{
    public abstract class TooltipTriggerBase : MonoBehaviour, ITooltipTargetBase
    {
        
        [SerializeField] protected string header = "";
        [TextArea] [SerializeField] protected string content;
        [SerializeField] private Transform tooltipStaticTarget;
        
        
        public virtual void ShowTooltipInfo()
        {
            TooltipManager.Instance.ShowTooltip(content,header,tooltipStaticTarget);
        }

        public virtual void HideTooltipInfo()
        {
            TooltipManager.Instance.HideTooltip();
        }
    }
}