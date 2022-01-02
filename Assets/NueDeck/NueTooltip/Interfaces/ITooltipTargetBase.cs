using NueTooltip.Core;
using UnityEngine;

namespace NueTooltip.Interfaces
{
    public interface ITooltipTargetBase
    {
        void ShowTooltipInfo(TooltipManager tooltipManager,string content,string header ="",Transform tooltipStaticTransform = null);

        void HideTooltipInfo();
    }
}