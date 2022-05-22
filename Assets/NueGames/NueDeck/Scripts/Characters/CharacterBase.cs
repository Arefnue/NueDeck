using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.Interfaces;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Characters
{
    public abstract class CharacterBase : MonoBehaviour, ICharacter
    {
        [Header("Base settings")]
        [SerializeField] private CharacterType characterType;

        [SerializeField] private Transform textSpawnRoot;
        public CharacterStats CharacterStats { get; protected set; }

        public CharacterType CharacterType => characterType;

        public Transform TextSpawnRoot => textSpawnRoot;

        public virtual void Awake()
        {
        }
        
        public virtual void BuildCharacter()
        {
            
        }
        
        protected virtual void OnDeath()
        {
            
        }

        public virtual void OnCardTargetHighlight()
        {
            
        }

        public virtual void OnCardOverHighlight()
        {
            
        }

        public virtual void OnCardPlayedOnMe()
        {
           
        }

        public  CharacterBase GetCharacterBase()
        {
            return this;
        }

        public CharacterType GetCharacterType()
        {
            return CharacterType;
        }
    }
}