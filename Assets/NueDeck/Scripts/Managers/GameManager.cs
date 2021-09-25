using System;
using System.Collections.Generic;
using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Data;
using NueDeck.Scripts.Data.Settings;
using UnityEngine;

namespace NueDeck.Scripts.Managers
{
    [DefaultExecutionOrder(-5)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        
        [SerializeField] private GameplayData gameplayData;
        public PersistentGameplayData PersistentGameplayData { get; private set; }

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

        public void InitGameplayData()
        {
            PersistentGameplayData = new PersistentGameplayData(gameplayData);
            
        }
    }
}
