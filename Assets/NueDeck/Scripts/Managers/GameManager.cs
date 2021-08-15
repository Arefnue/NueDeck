using System;
using System.Collections.Generic;
using System.Linq;
using NueDeck.Scripts.Card;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace NueDeck.Scripts.Managers
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        
        [Header("Settings")]
        public List<CardSO> allCardsList;
        public CardBase cardPrefab;
        
        [Header("Decks")]
        public List<int> myDeckIDList = new List<int>();
        public DeckSO initalDeck;

        public List<CardSO> choiceCardList;
        [HideInInspector] public List<CardBase> choiceContainer = new List<CardBase>();
        
        public float playerCurrentHealth=100;
        public float playerMaxHealth=100;
        public int currentGold;
        public string playerName;
        public int maxMana = 3;
        
        public bool isRandomHand;
        
        #region Setup

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        

        #endregion
        
        
        public CardBase BuildAndGetCard(int id,Transform parent)
        {
            var card = allCardsList.FirstOrDefault(x => x.myID == id);
            if (card)
            {
                var clone = Instantiate(cardPrefab, parent);
                clone.SetCard(card);
                return clone;
            }
            return null;
           
        }

        public void SetInitalHand()
        {
            myDeckIDList.Clear();
            if (isRandomHand)
            {
                for (int i = 0; i < 10; i++)
                    myDeckIDList.Add(allCardsList[Random.Range(0,allCardsList.Count)].myID);
            }
            else
            {
                initalDeck.cards.ForEach(x=>myDeckIDList.Add(x.myID));
            }
        }
        public void ResetManager()
        {
            choiceContainer?.Clear();
            playerCurrentHealth = 100;
            playerMaxHealth = 100;
            currentGold = 0;
        }
        public void ChangePlayerMaxHealth(float value)
        {
            playerMaxHealth += value;
            LevelManager.instance.playerController.myHealth.maxHealth = playerMaxHealth;
            LevelManager.instance.playerController.myHealth.ChangeHealthText();
        }
        public void ChangeScene(int target)
        {
            SceneManager.LoadScene(target);
        }
        public int GetCurrentLevel()
        {
            return SceneManager.GetActiveScene().buildIndex-3;
        }
        public void NextLevel()
        {
           
            var i = SceneManager.GetActiveScene().buildIndex + 1;
            
            if (i>=SceneManager.sceneCountInBuildSettings)
            {
                ResetManager();
                SceneManager.LoadScene(1);
            }
            else
            {
                SceneManager.LoadScene(i);
            }
        }
    }
}
