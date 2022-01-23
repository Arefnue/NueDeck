
using NueDeck.Scripts.UI;

namespace NueDeck.Scripts.Characters.Allies
{
    public class PlayerExample : AllyBase
    {
        public override void BuildCharacter()
        {
            base.BuildCharacter();
            if (UIManager.Instance != null)
                CharacterStats.OnHealthChanged += UIManager.Instance.informationCanvas.SetHealthText;
            CharacterStats.SetCurrentHealth(CharacterStats.CurrentHealth);
        }
    }
}