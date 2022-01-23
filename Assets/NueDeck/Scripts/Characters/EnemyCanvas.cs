using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NueDeck.Scripts.Characters
{
    public class EnemyCanvas : CharacterCanvas
    {
        public Image intentImage;
        public TextMeshProUGUI nextActionValueText;
        public Image IntentImage => intentImage;
        public TextMeshProUGUI NextActionValueText => nextActionValueText;
    }
}