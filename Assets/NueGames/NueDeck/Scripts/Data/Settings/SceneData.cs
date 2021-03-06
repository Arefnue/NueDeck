using UnityEngine;

namespace NueGames.NueDeck.Scripts.Data.Settings
{
    [CreateAssetMenu(fileName = "Scene Data", menuName = "NueDeck/Settings/Scene", order = 2)]
    public class SceneData : ScriptableObject
    {
        public int coreSceneIndex = 0;
        public int mainMenuSceneIndex = 1;
        public int mapSceneIndex = 2;
        public int combatSceneIndex = 3;
    }
}