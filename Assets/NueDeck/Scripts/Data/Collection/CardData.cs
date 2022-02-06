using System;
using System.Collections.Generic;
using System.Text;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Managers;
using NueExtentions;
using UnityEngine;

namespace NueDeck.Scripts.Data.Collection
{
    [CreateAssetMenu(fileName = "Card Data",menuName = "NueDeck/Data/Collection/Card",order = 0)]
    public class CardData : ScriptableObject
    {
        [Header("Card Profile")] 
        [SerializeField] private string id;
        [SerializeField] private string cardName;
        [SerializeField] private int manaCost;
        [SerializeField] private Sprite cardSprite;
        
        [Header("Action Settings")]
        [SerializeField] private ActionTarget actionTarget;
        [SerializeField] private bool usableWithoutTarget;
        [SerializeField] private List<CardActionData> cardActionDataList;
        
        [Header("Description")]
        [SerializeField] private List<CardDescriptionData> cardDescriptionDataList;
        [SerializeField] private List<SpecialKeywords> specialKeywordsList;
        
        [Header("Fx")]
        [SerializeField] private AudioActionType audioType;

        #region Encapsulation
        public string Id => id;
        public bool UsableWithoutTarget => usableWithoutTarget;
        public int ManaCost => manaCost;
        public string CardName => cardName;
        public ActionTarget MyTarget => actionTarget;
        public Sprite CardSprite => cardSprite;
        public List<CardActionData> CardActionDataList => cardActionDataList;
        public List<CardDescriptionData> CardDescriptionDataList => cardDescriptionDataList;
        public List<SpecialKeywords> KeywordsList => specialKeywordsList;
        public AudioActionType AudioType => audioType;
        public string MyDescription { get; set; }
        
        #endregion
        
        #region Methods
        public void UpdateDescription()
        {
            var str = new StringBuilder();
            
            foreach (var descriptionData in cardDescriptionDataList)
                str.Append(descriptionData.GetDescription());
            
            MyDescription = str.ToString();
        }
        #endregion
    }

    [Serializable]
    public class CardActionData
    {
        [SerializeField] private CardActionType myPlayerActionType;
        [SerializeField] private float value;
        public CardActionType MyPlayerActionType => myPlayerActionType;
        public float Value => value;
    }

    [Serializable]
    public class CardDescriptionData
    {
        [Header("Text")]
        [SerializeField] private bool useText = true;
        [SerializeField] private bool useTextColor;
        [SerializeField] private Color textColor = Color.black;
        [SerializeField] private string defaultText;
        
        [Header("Value")]
        [SerializeField] private bool useValue = true;
        [SerializeField] private bool useValueColor;
        [SerializeField] private Color valueColor = Color.black;
        [SerializeField] private int defaultValue;
        
        [Header("Modifer")]
        [SerializeField] private bool useModifer;
        [SerializeField] private StatusType modiferStatus;
        
        public string GetDescription()
        {
            var str = new StringBuilder();

            if (useText)
            {
                str.Append(defaultText);
                if (useTextColor) 
                    str.Replace(str.ToString(),ColorExtentions.ColorString(str.ToString(),textColor));
            }
            
            if (useValue)
            {
                var value = defaultValue;
                if (useModifer)
                {
                    var player = CombatManager.Instance.CurrentAlliesList[0];
                    if (player)
                    {
                        var modifer =player.CharacterStats.StatusDict[modiferStatus].StatusValue;
                        value += modifer;
                        
                        if (modifer!= 0)
                            str.Append("*");
                    }
                    
                }
                str.Append(value);
                if (useValueColor)  
                    str.Replace(str.ToString(),ColorExtentions.ColorString(str.ToString(),valueColor));
            }
            
            return str.ToString();
        }
        
     
    }
}