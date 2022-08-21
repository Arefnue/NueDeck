using NueGames.NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Utils
{
    public class RarityRoot : MonoBehaviour
    {
        [SerializeField] private RarityType rarity;

        public RarityType Rarity => rarity;
    }
}