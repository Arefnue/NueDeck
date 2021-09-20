using System;
using UnityEngine;

namespace NueDeck.Scripts.Characters
{
    public abstract class CharacterBase : MonoBehaviour
    {
        [Header("Base References")]
        public GameObject highlightObject;
        public CharacterData characterData;
        public CharacterHealth CharacterHealth { get; protected set; }
        public virtual void Awake()
        {
            CharacterHealth = new CharacterHealth(characterData.maxHealth);
            highlightObject.SetActive(false);
        }
        
        protected virtual void OnDeath()
        {
            
        }

    }
}