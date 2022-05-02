using UnityEngine;
using UnityEngine.Events;

namespace NueGames.NueDeck.Scripts.Utils
{
    public class TriggerOnSceneLoad : MonoBehaviour
    {
        public UnityEvent onLoad;
      
        private void Start()
        {
            onLoad?.Invoke();
        }
    }
}
