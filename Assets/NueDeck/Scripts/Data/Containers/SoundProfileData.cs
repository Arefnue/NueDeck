using System.Collections.Generic;
using NueDeck.Scripts.Enums;
using NueExtentions;
using UnityEngine;

namespace NueDeck.Scripts.Data.Containers
{
    [CreateAssetMenu(fileName = "Sound Profile", menuName = "Data/Containers/SoundProfile", order = 1)]
    public class SoundProfileData : ScriptableObject
    {
        public AudioActionType audioType;
        public List<AudioClip> randomClipList;
        
        public AudioClip GetRandomClip()
        {
            return randomClipList.Count>0 ? randomClipList.RandomItem():null;
        }
    }
}