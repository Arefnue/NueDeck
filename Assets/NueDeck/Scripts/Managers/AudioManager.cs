using UnityEngine;

namespace NueDeck.Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public AudioSource musicSource;
        public AudioSource sfxSource;
        public AudioSource buttonSource;
        
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        public void PlayMusic(AudioClip clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }

        public void PlayOneShot(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }
        
        public void PlayOneShotButton(AudioClip clip)
        {
            buttonSource.PlayOneShot(clip);
        }
    }
}
