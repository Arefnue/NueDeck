using NueTooltip.Interfaces;

namespace NueTooltip.Triggers
{
    public class TooltipTrigger3D : TooltipTriggerBase, I3DTooltipTarget
    {
        protected override void ShowTooltipInfo()
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