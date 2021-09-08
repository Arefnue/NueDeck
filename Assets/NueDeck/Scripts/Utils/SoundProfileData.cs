using System.Collections.Generic;
using UnityEngine;

namespace NueDeck.Scripts.Utils
{
    [CreateAssetMenu(fileName = "Sound Profile", menuName = "Data/Sound Profile", order = 3)]
    public class SoundProfileData : ScriptableObject
    {
        public List<AudioClip> randomClipList;
        
        public AudioClip GetRandomClip()
        {
            return randomClipList[Random.Range(0, randomClipList.Count)];
        }
    }
}