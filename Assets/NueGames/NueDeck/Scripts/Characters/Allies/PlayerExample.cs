using NueGames.NueDeck.Scripts.Managers;

namespace NueGames.NueDeck.Scripts.Characters.Allies
{
    public class PlayerExample : AllyBase
    {
        public override void BuildCharacter()
        {
            base.BuildCharacter();
            if (UIManager.Instance != null)
                CharacterStats.OnHealthChanged += UIManager.Instance.InformationCanvas.SetHealthText;
            CharacterStats.SetCurrentHealth(CharacterStats.CurrentHealth);
        }
    }
}