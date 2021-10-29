using NueDeck.Scripts.Managers;
using NueDeck.Scripts.UI.Reward;
using UnityEngine;

namespace NueDeck.Scripts.UI
{
    [DefaultExecutionOrder(-4)]
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        
        public CombatCanvas combatCanvas;
        public InformationCanvas informationCanvas;
        public RewardCanvas rewardCanvas;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
           
        }
        
        public void SetCanvas(CanvasBase targetCanvas,bool open,bool reset = false)
        {
            if (reset)
                targetCanvas.ResetCanvas();
            
            if (open)
                targetCanvas.OpenCanvas();
            else
                targetCanvas.CloseCanvas();
        }
        
    }
}
