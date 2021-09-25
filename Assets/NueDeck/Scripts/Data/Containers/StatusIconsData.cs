using System;
using System.Collections.Generic;
using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Data.Containers
{
    [CreateAssetMenu(fileName = "Status Icons", menuName = "Data/Containers/StatusIcons", order = 2)]
    public class StatusIconsData : ScriptableObject
    {
        public List<StatusIcon> statusIconList;
    }


    [Serializable]
    public class StatusIcon
    {
        public StatusType iconStatus;
        public Sprite iconSprite;
    }
}