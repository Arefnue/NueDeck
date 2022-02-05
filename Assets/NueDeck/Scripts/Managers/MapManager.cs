using System;
using System.Collections.Generic;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.UI;
using UnityEngine;

namespace NueDeck.Scripts.Managers
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private List<EncounterButton> encounterButtonList;

        public List<EncounterButton> EncounterButtonList => encounterButtonList;
        
        private void Start()
        {
            PrepareEncounters();
        }
        
        private void PrepareEncounters()
        {
            for (int i = 0; i < EncounterButtonList.Count; i++)
            {
                var btn = EncounterButtonList[i];
                if (GameManager.Instance.PersistentGameplayData.CurrentEncounterId == i)
                    btn.SetStatus(EncounterButtonStatus.Active);
                else if (GameManager.Instance.PersistentGameplayData.CurrentEncounterId > i)
                    btn.SetStatus(EncounterButtonStatus.Completed);
                else
                    btn.SetStatus(EncounterButtonStatus.Passive);
            }
        }
    }
}
