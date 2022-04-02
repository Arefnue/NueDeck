namespace NueDeck.Editor
{
    using UnityEditor;
    public class AutoLayerEditor : Editor
    {
        [MenuItem("NueDeck/Set Layers")]
        public static void OpenCardEditor()
        {
            new Layers().AddNewLayer("Card");
            new Layers().AddNewLayer("Character");
        }
    }
}