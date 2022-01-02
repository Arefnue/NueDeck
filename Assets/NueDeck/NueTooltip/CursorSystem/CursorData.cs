using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NueDeck.NueTooltip.CursorSystem
{
    [CreateAssetMenu(fileName = "Cursor Data", menuName = "Data/Containers/CursorData", order = 0)]
    public class CursorData : ScriptableObject
    {
        [SerializeField] private List<CursorProfile> cursorProfileList;
        public void SetCursor(CursorType targetType)
        {
            var targetCursor = cursorProfileList.FirstOrDefault(x => x.cursorType == targetType);
            Cursor.SetCursor(targetCursor?.cursorTexture, Vector3.zero, CursorMode.Auto);
        }
    }

    [Serializable]
    public class CursorProfile
    {
        public CursorType cursorType;
        public Texture2D cursorTexture;
    }
}