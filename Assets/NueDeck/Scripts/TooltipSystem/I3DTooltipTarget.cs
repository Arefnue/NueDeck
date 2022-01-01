namespace NueDeck.Scripts.TooltipSystem
{
    public interface I3DTooltipTarget : ITooltipTargetBase
    { 
        void OnMouseEnter(); 
        void OnMouseExit();
    }
}