using System;
using System.Collections.Generic;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.UI;
using UnityEngine;

namespace NueDeck.Scripts.Data.Containers
{
    [CreateAssetMenu(fileName = "Status Icons", menuName = "Data/Containers/StatusIcons", order = 2)]
    public class StatusIconsData : ScriptableObject
    {
        [SerializeField] private StatusIconBase statusIconBasePrefab;
        [SerializeField] private List<StatusIconData> statusIconList;

        public StatusIconBase StatusIconBasePrefab => statusIconBasePrefab;
        public List<StatusIconData> StatusIconList => statusIconList;
    }


    [Serializable]
    public class StatusIconData
    {
        [SerializeField] private StatusType iconStatus;
        [SerializeField] private Sprite iconSprite;
        [SerializeField] private List<SpecialKeywords> specialKeywords;
        public StatusType IconStatus => iconStatus;
        public Sprite IconSprite => iconSprite;
        public List<SpecialKeywords> SpecialKeywords => specialKeywords;
    }
}