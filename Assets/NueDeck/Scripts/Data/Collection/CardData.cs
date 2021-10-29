using System;
using System.Collections.Generic;
using NueDeck.Scripts.Data.Containers;
using NueDeck.Scripts.Data.Settings;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Managers;
using NueExtentions;
using UnityEngine;

namespace NueDeck.Scripts.Data.Collection
{
    [CreateAssetMenu(fileName = "Card Data",menuName = "Data/Collection/Card",order = 0)]
    public class CardData : ScriptableObject
    {
        [Header("Card Defaults")]
        public int myID;
        public ActionTargets myTargets;
        public bool usableWithoutTarget;
        public int myManaCost;
        public string myName;

        public string MyDescription { get; set; }

        public Sprite mySprite;
        public List<CardActionData> actionList;
        public List<DescriptionData> descriptionDataList;
        public SoundProfileData mySoundProfileData;

        public void UpdateDescription()
        {
            MyDescription = "";

            foreach (var descriptionData in descriptionDataList)
            {
                MyDescription += descriptionData.GetDescription();
            }
        }

    }

    [Serializable]
    public class CardActionData
    {
        public CardActionType myPlayerActionType;
        public float value;
        
    }

    [Serializable]
    public class DescriptionData
    {
        [SerializeField]private bool useText = true;
        [SerializeField]private bool useValue = true;
        [SerializeField]private string text;
        [SerializeField]private int defaultValue;
        
        [Header("Color")]
        [SerializeField]private bool useValueColor;
        [SerializeField]private Color valueColor;
        [SerializeField]private bool useTextColor;
        [SerializeField]private Color textColor;
        
        
        [Header("Modifer")]
        [SerializeField]private bool useModifer;
        [SerializeField]private StatusType modiferStatus;

        public string GetDescription()
        {
            var str = "";

            if (useText)
            {
                str += text;
                if (useTextColor) str = ColorExtentions.ColorString(str,textColor);
            }
            
            if (useValue)
            {
                var value = defaultValue;
                if (useModifer)
                {
                   
                    var player = CombatManager.instance.currentAllies[0];
                    if (player)
                    {
                        var modifer =player.CharacterStats.statusDict[modiferStatus].StatusValue;
                        value += modifer;
                        if (modifer!= 0)
                        {
                            str += "*";
                        }
                        
                    }
                    
                }
                str += value;
                if (useValueColor) str = ColorExtentions.ColorString(str,valueColor);
            }
            
            return str;
        }
        
     
    }
}