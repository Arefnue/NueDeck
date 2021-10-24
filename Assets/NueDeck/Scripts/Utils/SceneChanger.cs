using NueDeck.Scripts.Managers;
using NueDeck.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NueDeck.Scripts.Utils
{
    public class SceneChanger : MonoBehaviour
    {
        public void OpenMainMenuScene()
        {
            SceneManager.LoadScene(GameManager.instance.SceneData.mainMenuSceneIndex);
            UIManager.instance.SetCanvas(UIManager.instance.combatCanvas,false,true);
            UIManager.instance.SetCanvas(UIManager.instance.informationCanvas,false,true);
            UIManager.instance.SetCanvas(UIManager.instance.rewardCanvas,false,true);
            GameManager.instance.InitGameplayData();
            GameManager.instance.SetInitalHand();
        }

        public void OpenMapScene()
        {
            SceneManager.LoadScene(GameManager.instance.SceneData.mapSceneIndex);
            UIManager.instance.SetCanvas(UIManager.instance.combatCanvas,false,true);
            UIManager.instance.SetCanvas(UIManager.instance.informationCanvas,true,false);
            UIManager.instance.SetCanvas(UIManager.instance.rewardCanvas,false,true);
        }

        public void OpenCombatScene()
        {
            SceneManager.LoadScene(GameManager.instance.SceneData.combatSceneIndex);
            UIManager.instance.SetCanvas(UIManager.instance.combatCanvas,false,true);
            UIManager.instance.SetCanvas(UIManager.instance.informationCanvas,true,false);
            UIManager.instance.SetCanvas(UIManager.instance.rewardCanvas,false,true);
        }
        
        
        public void ChangeScene(int sceneId)
        {

            if (sceneId == GameManager.instance.SceneData.mainMenuSceneIndex)
                OpenMainMenuScene();
            else if (sceneId == GameManager.instance.SceneData.mapSceneIndex)
                OpenMapScene();
            else if (sceneId == GameManager.instance.SceneData.combatSceneIndex)
                OpenCombatScene();
            else
                SceneManager.LoadScene(sceneId);
        }

        public void ExitApp()
        {
            GameManager.instance.ExitApp();
            Application.Quit();
        }
        
    }
}
