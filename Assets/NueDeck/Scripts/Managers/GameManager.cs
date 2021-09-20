using System;
using System.Collections.Generic;
using NueDeck.Scripts.Characters;
using UnityEngine;

namespace NueDeck.Scripts.Managers
{
    [DefaultExecutionOrder(-5)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        
        public List<AllyBase> allyList;
        
        
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
