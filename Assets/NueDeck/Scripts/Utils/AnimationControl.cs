using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.Utils
{
    public class AnimationControl : MonoBehaviour
    {
        public void ChangeScene()
        {
            GameManager.instance.ChangeScene(3);
        }
    }
}
