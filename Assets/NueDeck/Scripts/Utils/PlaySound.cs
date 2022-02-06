using NueDeck.Scripts.Data;
using NueDeck.Scripts.Data.Containers;
using NueDeck.Scripts.Data.Settings;
using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.Utils
{
    public class PlaySound : MonoBehaviour
    {
        [SerializeField] private SoundProfileData soundProfileData;
        private SoundProfileData SoundProfileData => soundProfileData;
        public void PlaySfx() => AudioManager.Instance.PlayOneShot(SoundProfileData.GetRandomClip());
        public void PlayButton() => AudioManager.Instance.PlayOneShotButton(SoundProfileData.GetRandomClip());
    }
}
