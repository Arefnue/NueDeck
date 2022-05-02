using NueGames.NueDeck.ThirdParty.NueTooltip.Core;
using NueGames.NueDeck.ThirdParty.NueTooltip.Interfaces;

namespace NueGames.NueDeck.ThirdParty.NueTooltip.Triggers
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