using NueTooltip.Core;
using NueTooltip.Interfaces;
using UnityEngine.EventSystems;

namespace NueTooltip.Triggers
{
    public class TooltipTrigger2D : TooltipTriggerBase,I2DTooltipTarget
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
           ShowTooltipInfo();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideTooltipInfo(TooltipManager.Instance);
        }
    }
}