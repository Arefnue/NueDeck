using System;
using System.Collections.Generic;
using System.Linq;
using NueDeck.Scripts.Data.Containers;
using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField]private AudioSource musicSource;
        [SerializeField]private AudioSource sfxSource;
        [SerializeField]private AudioSource buttonSource;
        
        [SerializeField] private List<SoundProfileData> soundProfileDataList;
        
        private Dictionary<AudioActionType, SoundProfileData> _audioDict = new Dictionary<AudioActionType, SoundProfileData>();

        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            
            for (int i = 0; i < Enum.GetValues(typeof(AudioActionType)).Length; i++)
            {
                _audioDict.Add((AudioActionType)i,soundProfileDataList.FirstOrDefault(x=>x.audioType == (AudioActionType)i));
            }

        }


        public void PlayMusic(AudioClip clip)
        {
            if (clip)
            {
                musicSource.clip = clip;
                musicSource.Play();
            }
        }

        public void PlayMusic(AudioActionType type)
        {
            var clip = _audioDict[type].GetRandomClip();
            if (clip)
                PlayMusic(clip);
            
        }

        public void PlayOneShot(AudioActionType type)
        {
            var clip = _audioDict[type].GetRandomClip();
            if (clip)
                PlayOneShot(clip);
            
        }

        public void PlayOneShotButton(AudioActionType type)
        {
            var clip = _audioDict[type].GetRandomClip();
            if (clip)
                PlayOneShotButton(clip);
           
        }

        public void PlayOneShot(AudioClip clip)
        {
            if (clip)
                sfxSource.PlayOneShot(clip);
        }
        
        public void PlayOneShotButton(AudioClip clip)
        {
            if (clip)
                buttonSource.PlayOneShot(clip);
        }
    }
}
