using NueGames.NueDeck.Scripts.Data.Settings;
using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NueGames.NueDeck.Scripts.Utils
{
    [DefaultExecutionOrder(-11)]
    public class CoreLoader : MonoBehaviour
    {
        private void Awake()
        {
            if (!GameManager.Instance)
                SceneManager.LoadScene("NueCore", LoadSceneMode.Additive);
            Destroy(gameObject);
        }
    }
}