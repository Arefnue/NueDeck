using NueGames.NueDeck.Scripts.Data.Collection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace NueGames.NueDeck.Editor
{
    public class CardDataAssetHandler
    {
        [OnOpenAsset]
        public static bool OpenEditor(int instanceId, int line)
        {
            CardData obj = EditorUtility.InstanceIDToObject(instanceId) as CardData;
            if (obj != null)
            {
                CardEditorWindow.OpenCardEditor(obj);
                return true;
            }
            return false;
        }
    }
    
    [CustomEditor(typeof(CardData))]
    public class CardDataCustomEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Open in editor"))
            {
                CardEditorWindow.OpenCardEditor((CardData)target);
            }
        }
    }
}