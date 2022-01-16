using NueTooltip.Core;
using NueTooltip.Interfaces;

namespace NueTooltip.Triggers
{
    public class TooltipTrigger3D : TooltipTriggerBase, I3DTooltipTarget
    {
        public void OnMouseEnter()
        {
           ShowTooltipInfo();
        }

        public void OnMouseExit()
        {
            HideTooltipInfo(TooltipManager.Instance);
        }
    }
}