using System;
using NueDeck.Scripts.Data;
using NueDeck.Scripts.Data.Containers;
using NueDeck.Scripts.Data.Settings;
using NueDeck.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace NueDeck.Scripts.Utils
{
    [RequireComponent(typeof(Button))]
    public class ButtonSoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundProfileData soundProfileData;

        private Button _btn;
        private SoundProfileData SoundProfileData => soundProfileData;
        private void Awake()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(PlayButton);
        }
        
        public void PlayButton() => AudioManager.Instance.PlayOneShotButton(SoundProfileData.GetRandomClip());
    }
}
