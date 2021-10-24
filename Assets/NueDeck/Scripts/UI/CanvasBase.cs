using UnityEngine;

namespace NueDeck.Scripts.UI
{
    public class CanvasBase : MonoBehaviour
    {

        public virtual void OpenCanvas()
        {
            gameObject.SetActive(true);
        }


        public virtual void CloseCanvas()
        {
            gameObject.SetActive(false);
        }

        public virtual void ResetCanvas()
        {
            
        }
    }
}