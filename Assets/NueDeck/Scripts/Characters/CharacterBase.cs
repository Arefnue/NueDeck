using NueDeck.Scripts.Data;
using NueDeck.Scripts.Data.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace NueDeck.Scripts.Characters
{
    public abstract class CharacterBase : MonoBehaviour
    {
        public CharacterStats CharacterStats { get; protected set; }
        
        public virtual void Awake()
        {
        }

        public virtual void BuildCharacter()
        {
            
        }
        
        protected virtual void OnDeath()
        {
            
        }

    }
}