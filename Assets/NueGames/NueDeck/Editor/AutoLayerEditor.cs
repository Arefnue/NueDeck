using NueGames.NueDeck.Scripts.Utils;
using UnityEditor;

namespace NueGames.NueDeck.Editor
{
    public class AutoLayerEditor : UnityEditor.Editor
    {
#if UNITY_EDITOR
        

        [MenuItem("NueDeck/Set Layers")]
        public static void OpenCardEditor()
        {
            new Layers().AddNewLayer("Character");
            new Layers().AddNewLayer("Card");
        }
#endif
    }
}