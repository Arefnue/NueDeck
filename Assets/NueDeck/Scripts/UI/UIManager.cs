using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.UI
{
    [DefaultExecutionOrder(-4)]
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        
        public CombatCanvas combatCanvas;
        public InformationCanvas informationCanvas;
        
      

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

    }
}
