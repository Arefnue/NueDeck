using UnityEngine;

namespace NueDeck.Scripts.TooltipSystem
{
    public class TooltipTrigger3D : TooltipTriggerBase, I3DTooltipTarget
    {
        public override void ShowTooltipInfo()
        {
            base.ShowTooltipInfo();
        }

        public override void HideTooltipInfo()
        {
            base.HideTooltipInfo();
        }

        public void OnMouseEnter()
        {
           ShowTooltipInfo();
        }

        public void OnMouseExit()
        {
            HideTooltipInfo();
        }
    }
}