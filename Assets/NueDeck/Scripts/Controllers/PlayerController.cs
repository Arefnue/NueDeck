using NueDeck.Scripts.Managers;
using NueDeck.Scripts.Utils;
using UnityEngine;

namespace NueDeck.Scripts.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        // [HideInInspector] public Health myHealth;
       
        public GameObject playerHighlight;
        public Transform fxParent;
        
        private void Awake()
        {
            // myHealth = GetComponent<Health>();
            // myHealth.deathAction += OnDeath;
           
            playerHighlight.SetActive(false);
        }

       

        private void OnDeath()
        {
            LevelManager.instance.OnPlayerDeath();
        }
    }
}