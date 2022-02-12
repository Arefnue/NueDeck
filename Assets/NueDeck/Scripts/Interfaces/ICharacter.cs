using NueDeck.Scripts.Characters;
using NueDeck.Scripts.Enums;

namespace NueDeck.Scripts.Interfaces
{
    public interface ICharacter
    {
        public void OnCardTargetHighlight();
        public void OnCardOverHighlight();
        public void OnCardPlayedOnMe();

        public CharacterBase GetCharacterBase();
        public CharacterType GetCharacterType();

    }
}