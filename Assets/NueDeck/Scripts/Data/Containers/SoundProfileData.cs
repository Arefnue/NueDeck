using System.Collections.Generic;
using NueDeck.Scripts.Enums;
using NueExtentions;
using UnityEngine;

namespace NueDeck.Scripts.Data.Containers
{
    [CreateAssetMenu(fileName = "Sound Profile", menuName = "Data/Containers/SoundProfile", order = 1)]
    public class SoundProfileData : ScriptableObject
    {
        [SerializeField] private AudioActionType audioType;
        [SerializeField] private List<AudioClip> randomClipList;

        public AudioActionType AudioType => audioType;

        public List<AudioClip> RandomClipList => randomClipList;

        public AudioClip GetRandomClip() => RandomClipList.Count>0 ? RandomClipList.RandomItem():null;
    }
}