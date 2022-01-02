using UnityEngine;

namespace NueDeck.Scripts.TooltipSystem
{
    public interface ITooltipTargetBase
    {
        void ShowTooltipInfo(TooltipManager tooltipManager,string content,string header ="",Transform tooltipStaticTransform = null);

        void HideTooltipInfo();
    }
}