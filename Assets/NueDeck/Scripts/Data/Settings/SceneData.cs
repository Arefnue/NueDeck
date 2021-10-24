using UnityEngine;

namespace NueDeck.Scripts.Data.Settings
{
    [CreateAssetMenu(fileName = "Scene Data", menuName = "Data/Settings/Scene", order = 2)]
    public class SceneData : ScriptableObject
    {
        public int mainMenuSceneIndex = 0;
        public int mapSceneIndex = 1;
        public int combatSceneIndex = 2;
    }
}