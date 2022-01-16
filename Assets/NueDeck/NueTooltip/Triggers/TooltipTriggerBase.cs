using NueTooltip.Core;
using NueTooltip.CursorSystem;
using NueTooltip.Interfaces;
using UnityEngine;

namespace NueTooltip.Triggers
{
    public abstract class TooltipTriggerBase : MonoBehaviour, ITooltipTargetBase
    {
        
        [SerializeField] protected string headerText = "";
        [TextArea] [SerializeField] protected string contentText;
        [SerializeField] private Transform tooltipStaticTargetTransform;
        [SerializeField] private CursorType cursorType = CursorType.Default;
        
        protected virtual void ShowTooltipInfo()
        {
            ShowTooltipInfo(TooltipManager.Instance,contentText,headerText,tooltipStaticTargetTransform,cursorType);
        }

        public void ShowTooltipInfo(TooltipManager tooltipManager, string content, string header = "", Transform tooltipStaticTransform = null,CursorType targetCursor = CursorType.Default,Camera cam = null)
        {
            tooltipManager.ShowTooltip(content,header,tooltipStaticTransform,targetCursor,cam);
        }

        public virtual void HideTooltipInfo(TooltipManager tooltipManager)
        {
            TooltipManager.Instance.HideTooltip();
        }
    }
}