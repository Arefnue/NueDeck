using NueGames.NueDeck.Scripts.Characters;
using NueGames.NueDeck.Scripts.Enums;

namespace NueGames.NueDeck.Scripts.Interfaces
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