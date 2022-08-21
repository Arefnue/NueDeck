using NueGames.NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Utils.Background
{
    public class BackgroundRoot : MonoBehaviour
    {
        [SerializeField] private BackgroundTypes backgroundType;

        public BackgroundTypes BackgroundType => backgroundType;
    }
}