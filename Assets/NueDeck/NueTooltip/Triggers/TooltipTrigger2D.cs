﻿using NueTooltip.Interfaces;
using UnityEngine.EventSystems;

namespace NueTooltip.Triggers
{
    public class TooltipTrigger2D : TooltipTriggerBase,I2DTooltipTarget
    {
        protected override void ShowTooltipInfo()
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