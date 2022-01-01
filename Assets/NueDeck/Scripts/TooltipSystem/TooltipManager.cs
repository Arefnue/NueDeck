using System;
using UnityEngine;

namespace NueDeck.Scripts.TooltipSystem
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance;
        
        [SerializeField] private TooltipController tooltipController;
        public TooltipController TooltipController => tooltipController;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        
        
        
    }
}