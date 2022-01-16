using System;
using UnityEngine;
using UnityEngine.Events;

namespace NueDeck.Scripts.Utils
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
