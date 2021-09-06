using System;
using System.Collections.Generic;
using NueDeck.Scripts.Controllers;
using NueDeck.Scripts.Utils;
using UnityEngine;

namespace NueDeck.Scripts.Card
{
    public class CardSO : ScriptableObject
    {
        public enum CardTargets
        {
            Enemy,
            Player
        }

        [Header("Card Defaults")]
        public int myID;
        public CardTargets myTargets;
       
        public int myManaCost;
        public string myName;
        [TextArea]
        public string myDescription;
        public Sprite mySprite;
        public List<ActionSO> actionList;

        public void PlayCard(EnemyBase targetEnemy)
        {
            foreach (var actionSO in actionList)
            {
                actionSO.PlayCard(targetEnemy);
            }
        }

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