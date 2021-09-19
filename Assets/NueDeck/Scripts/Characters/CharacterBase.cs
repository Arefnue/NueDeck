using System;
using UnityEngine;

namespace NueDeck.Scripts.Characters
{
    public abstract class CharacterBase : MonoBehaviour
    {
        [Header("Base References")]
        public GameObject highlightObject;
        public virtual void Awake()
        {
            highlightObject.SetActive(false);
        }
        
        protected virtual void OnDeath()
        {
            
        }
    }
}