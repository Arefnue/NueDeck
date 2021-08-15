using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.Utils
{
    public class PlaySound : MonoBehaviour
    {
        public SoundProfile myProfile;

        public void PlaySfx()
        {
            AudioManager.instance.PlayOneShot(myProfile.GetRandomClip());
        }

        public void PlayButton()
        {
            AudioManager.instance.PlayOneShotButton(myProfile.GetRandomClip());
        }
    }
}
