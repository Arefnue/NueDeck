using NueTooltip.Core;
using NueTooltip.CursorSystem;
using UnityEngine;

namespace NueTooltip.Interfaces
{
    public interface ITooltipTargetBase
    {
        void ShowTooltipInfo(TooltipManager tooltipManager,string content,string header ="",Transform tooltipStaticTransform = null,CursorType targetCursor = CursorType.Default);

        void HideTooltipInfo(TooltipManager tooltipManager);
    }
}