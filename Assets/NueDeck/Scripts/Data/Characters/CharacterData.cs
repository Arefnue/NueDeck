using UnityEngine;

namespace NueDeck.Scripts.Data.Characters
{
    public class CharacterData : ScriptableObject
    {
        public int characterID;
        public string characterName;
        [TextArea]
        public string characterDescription;
        public int maxHealth;
        
    }
}