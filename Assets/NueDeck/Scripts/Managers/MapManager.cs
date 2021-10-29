using System;
using System.Collections.Generic;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.UI;
using UnityEngine;

namespace NueDeck.Scripts.Managers
{
    public class MapManager : MonoBehaviour
    {
        public List<EncounterButton> encounterButtonList;


        private void Start()
        {
            PrepareEncounters();
        }


        public void PrepareEncounters()
        {
            for (int i = 0; i < encounterButtonList.Count; i++)
            {
                var btn = encounterButtonList[i];
                if (GameManager.instance.PersistentGameplayData.CurrentEncounterId == i)
                {
                    btn.SetStatus(EncounterButtonStatus.Active);
                }
                else if (GameManager.instance.PersistentGameplayData.CurrentEncounterId > i)
                {
                    btn.SetStatus(EncounterButtonStatus.Completed);
                }
                else
                {
                    btn.SetStatus(EncounterButtonStatus.Passive);
                }
            }
        }
    }
}
