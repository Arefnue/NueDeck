using System.Collections.Generic;
using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.NueExtentions;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Data.Containers
{
    [CreateAssetMenu(fileName = "Sound Profile", menuName = "NueDeck/Containers/SoundProfile", order = 1)]
    public class SoundProfileData : ScriptableObject
    {
        [SerializeField] private AudioActionType audioType;
        [SerializeField] private List<AudioClip> randomClipList;

        public AudioActionType AudioType => audioType;

        public List<AudioClip> RandomClipList => randomClipList;

        public AudioClip GetRandomClip() => RandomClipList.Count>0 ? RandomClipList.RandomItem():null;
    }
}