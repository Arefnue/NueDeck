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
        public bool encounterRandomlyAtStage; 
        public List<EnemyEncounterStage> enemyEncounterList;

        public EnemyEncounter GetEnemyEncounter(int stageId = 0,int encounterId =0,bool isFinal = false)
        {
            var selectedStage = enemyEncounterList.First(x => x.stageId == stageId);
            if (isFinal) return selectedStage.bossEncounterList.RandomItem();
           
            return encounterRandomlyAtStage
                ? selectedStage.enemyEncounterList.RandomItem()
                : selectedStage.enemyEncounterList[encounterId] ?? selectedStage.enemyEncounterList.RandomItem();
        }
        
    }


    [Serializable]
    public class EnemyEncounterStage
    {
        public string name;
        public int stageId;
        public List<EnemyEncounter> bossEncounterList;
        public List<EnemyEncounter> enemyEncounterList;
    }
    
    
    [Serializable]
    public class EnemyEncounter
    {
        public List<EnemyBase> enemyList;
    }
}