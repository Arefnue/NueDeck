using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace NueGames.NueDeck.Scripts.Utils
{
    public class RandomToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        private GameManager GameManager => GameManager.Instance;
        public void CheckToggle()
        {
            GameManager.PersistentGameplayData.IsRandomHand = toggle.isOn;
            GameManager.SetInitalHand();
        }
    }
}