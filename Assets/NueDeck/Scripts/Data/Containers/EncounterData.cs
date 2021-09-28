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

        public EnemyEncounter GetEnemyEncounter(int stageId = 0,int encounterId =0)
        {
            var selectedStage = enemyEncounterList.First(x => x.stageId == stageId);
            return encounterRandomlyAtStage
                ? selectedStage.enemyEncounterList.RandomItem()
                : selectedStage.enemyEncounterList[encounterId];
        }
        
    }


    [Serializable]
    public class EnemyEncounterStage
    {
        public string name;
        public int stageId;
        public List<EnemyEncounter> enemyEncounterList;
    }
    
    
    [Serializable]
    public class EnemyEncounter
    {
        public List<EnemyBase> enemyList;
    }
}