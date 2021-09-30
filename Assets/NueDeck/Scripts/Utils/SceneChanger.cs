using UnityEngine;
using UnityEngine.SceneManagement;

namespace NueDeck.Scripts.Utils
{
    public class SceneChanger : MonoBehaviour
    {
        
        public void ChangeScene(int sceneId)
        {
            SceneManager.LoadScene(sceneId);
        }

        public void ExitApp()
        {
            Application.Quit();
        }
        
    }
}
