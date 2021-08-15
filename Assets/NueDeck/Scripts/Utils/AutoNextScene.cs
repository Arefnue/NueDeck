using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.Utils
{
    public class AutoNextScene : MonoBehaviour
    {
        private void Start()
        {
            GameManager.instance.ChangeScene(1);
        }
    }
}
