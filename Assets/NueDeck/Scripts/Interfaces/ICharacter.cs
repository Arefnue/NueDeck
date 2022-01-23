using NueDeck.Scripts.Characters;

namespace NueDeck.Scripts.Interfaces
{
    public interface ICharacter
    {
        public void OnCardTargetHighlight();
        public void OnCardOverHighlight();
        public void OnCardPlayedOnMe();

        public CharacterBase GetCharacterBase();
        
    }
}