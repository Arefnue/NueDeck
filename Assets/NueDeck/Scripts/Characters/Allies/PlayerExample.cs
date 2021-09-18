using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.Characters.Allies
{
    public class PlayerExample : AllyBase
    {
      
        public GameObject playerHighlight;
        public Transform fxParent;
        
        private void Awake()
        {
            playerHighlight.SetActive(false);
        }
        
        private void OnDeath()
        {
            LevelManager.instance.OnPlayerDeath();
        }
    }
}