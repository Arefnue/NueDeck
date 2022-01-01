using System.Collections;
using NueDeck.Scripts.UI.Reward;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NueDeck.Scripts.UI
{
    [DefaultExecutionOrder(-4)]
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        
        public CombatCanvas combatCanvas;
        public InformationCanvas informationCanvas;
        public RewardCanvas rewardCanvas;
       
        [SerializeField] private CanvasGroup fader;
        [SerializeField] private float fadeSpeed = 1f;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
           
        }
        
        public void SetCanvas(CanvasBase targetCanvas,bool open,bool reset = false)
        {
            if (reset)
                targetCanvas.ResetCanvas();
            
            if (open)
                targetCanvas.OpenCanvas();
            else
                targetCanvas.CloseCanvas();
        }
        
        public void ChangeScene(int index)
        {
            StartCoroutine(ChangeSceneRoutine(index));
        }

        private IEnumerator ChangeSceneRoutine(int index)
        {
            SceneManager.LoadScene(index);
            yield return StartCoroutine(Fade(false));
        }
        
        public IEnumerator Fade(bool isIn)
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = isIn ? 0f : 1f;

            while (true)
            {
                timer += Time.deltaTime* (isIn ? fadeSpeed : -fadeSpeed);
                
                fader.alpha = timer;
                
                if (timer>=1f)  break;
              
                yield return waitFrame;
            }
        }
    }
}
