using UnityEngine;
using UnityEngine.EventSystems;

namespace NueDeck.Scripts.TooltipSystem
{
    public class TooltipTrigger2D : TooltipTriggerBase,I2DTooltipTarget
    {
        public override void ShowTooltipInfo()
        {
            base.ShowTooltipInfo();
        }

        public override void HideTooltipInfo()
        {
            base.HideTooltipInfo();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           ShowTooltipInfo();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideTooltipInfo();
        }
    }
}