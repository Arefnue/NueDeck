using System;
using System.Collections.Generic;
using NueDeck.Scripts.Card;
using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Data;
using NueDeck.Scripts.Data.Containers;
using NueDeck.Scripts.Data.Settings;
using NueDeck.Scripts.EnemyBehaviour;
using UnityEngine;

namespace NueDeck.Scripts.Managers
{
    [DefaultExecutionOrder(-5)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        
        [Header("Settings")] 
        public Camera mainCam;

        [SerializeField] private GameplayData gameplayData;
        [SerializeField] private EncounterData encounterData;

        public EncounterData EncounterData => encounterData;
        public GameplayData GameplayData => gameplayData;
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
            
            CardActionProcessor.Initialize();
            EnemyActionProcessor.Initialize();
            InitGameplayData();
        }
        

        public void InitGameplayData()
        {
            PersistentGameplayData = new PersistentGameplayData(gameplayData);
        }
        
    }
}
