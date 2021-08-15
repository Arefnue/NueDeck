using System;
using System.Collections.Generic;
using NueDeck.Scripts.Utils;
using UnityEngine;

namespace NueDeck.Scripts.Card
{
    [CreateAssetMenu(fileName = "Card Profile", menuName = "Playable Card", order = 0)]
    public class CardSO : ScriptableObject
    {
        #region Card Enums
        public enum CardTargets
        {
            Enemy,
            Player
        }
        
        #endregion

        public int myID;
        public CardTargets myTargets;
        public List<PlayerAction> playerActionList;
        public int myManaCost;
        public string myName;
        [TextArea]
        public string myDescription;
        public Sprite mySprite;
        
    }

    [Serializable]
    public class PlayerAction
    {
        public enum PlayerActionType
        {
            Attack,
            Heal,
            Block,
            IncreaseStr,
            IncreaseMaxHealth,
            Draw,
            ReversePoisonDamage,
            ReversePoisonHeal,
            IncreaseMana,
            StealMaxHealth
        }
        //todo sözlük yap
        
        public PlayerActionType myPlayerActionType;
        public float value;
        public SoundProfile mySoundProfile;
    }
}