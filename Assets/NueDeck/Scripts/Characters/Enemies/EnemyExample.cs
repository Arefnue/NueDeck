using NueDeck.Scripts.Data;
using NueDeck.Scripts.Data.Containers;
using NueDeck.Scripts.Data.Settings;
using NueDeck.Scripts.Managers;
using NueDeck.Scripts.Utils;
using UnityEngine;

namespace NueDeck.Scripts.Characters.Enemies
{
    public class EnemyExample : EnemyBase
    {
        [Header("References")]
        public SoundProfileData deathSoundProfileData;
        protected override void OnDeath()
        { 
            base.OnDeath();
            AudioManager.instance.PlayOneShot(deathSoundProfileData.GetRandomClip());
            Destroy(gameObject);
        }


    }
}