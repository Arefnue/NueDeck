using NueDeck.Scripts.Data;
using NueDeck.Scripts.Data.Characters;
using UnityEngine;

namespace NueDeck.Scripts.Characters
{
    public abstract class CharacterBase : MonoBehaviour
    {
        public CharacterHealth CharacterHealth { get; protected set; }
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