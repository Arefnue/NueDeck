using System;
using System.Collections.Generic;
using System.Linq;
using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Data.Characters;
using NueExtentions;
using UnityEngine;

namespace NueDeck.Scripts.Data.Containers
{
    [CreateAssetMenu(fileName = "Encounter Data", menuName = "Data/Containers/EncounterData", order = 4)]
    public class EncounterData : ScriptableObject
    {
        [Header("Settings")] 
        [SerializeField] private bool encounterRandomlyAtStage;
        [SerializeField] private List<EnemyEncounterStage> enemyEncounterList;

        public bool EncounterRandomlyAtStage => encounterRandomlyAtStage;
        public List<EnemyEncounterStage> EnemyEncounterList => enemyEncounterList;

        public EnemyEncounter GetEnemyEncounter(int stageId = 0,int encounterId =0,bool isFinal = false)
        {
            var selectedStage = EnemyEncounterList.First(x => x.StageId == stageId);
            if (isFinal) return selectedStage.BossEncounterList.RandomItem();
           
            return EncounterRandomlyAtStage
                ? selectedStage.EnemyEncounterList.RandomItem()
                : selectedStage.EnemyEncounterList[encounterId] ?? selectedStage.EnemyEncounterList.RandomItem();
        }
        
    }


    [Serializable]
    public class EnemyEncounterStage
    {
        [SerializeField] private string name;
        [SerializeField] private int stageId;
        [SerializeField] private List<EnemyEncounter> bossEncounterList;
        [SerializeField] private List<EnemyEncounter> enemyEncounterList;
        public string Name => name;
        public int StageId => stageId;
        public List<EnemyEncounter> BossEncounterList => bossEncounterList;
        public List<EnemyEncounter> EnemyEncounterList => enemyEncounterList;
    }
    
    
    [Serializable]
    public class EnemyEncounter
    {
        [SerializeField] private List<EnemyBase> enemyList;
        public List<EnemyBase> EnemyList => enemyList;
    }
}