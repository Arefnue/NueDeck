
using NueDeck.Scripts.UI;

namespace NueDeck.Scripts.Characters.Allies
{
    public class PlayerExample : AllyBase
    {
        public override void BuildCharacter()
        {
            base.BuildCharacter();
            CharacterHealth.OnHealthChanged += UIManager.instance.informationCanvas.SetHealthText;
            CharacterHealth.SetCurrentHealth(CharacterHealth.CurrentHealth);
        }
    }
}