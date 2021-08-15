using NueDeck.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NueDeck.Scripts.Utils
{
    public class MainMenu : MonoBehaviour
    {
        public bool isBroken;
        private void Start()
        {
            if (!isBroken)
            {
                AudioManager.instance.PlayMusic(AudioManager.instance.bgMusic);
            }
           
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void PlayGame(int sceneIndex)
        {
            if (isBroken)
            {
                SceneManager.LoadScene(sceneIndex);
            }
            else
            {
                GameManager.instance.SetInitalHand();
                SceneManager.LoadScene(sceneIndex);
            }
           
        }

        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        
        public void SetToggle(Toggle targetToggle)
        {
            GameManager.instance.isRandomHand = targetToggle.isOn;
        }
    }
}
