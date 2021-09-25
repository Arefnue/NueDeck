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
        public StatusIcon statusIconPrefab;
        public List<StatusIconData> statusIconList;
    }


    [Serializable]
    public class StatusIconData
    {
        public StatusType iconStatus;
        public Sprite iconSprite;
    }
}