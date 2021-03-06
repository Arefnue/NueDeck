using System;
using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Utils
{
    public class InventoryHelper : MonoBehaviour
    {
        [SerializeField] private InventoryTypes inventoryType;


        public void OpenInventory()
        {
            switch (inventoryType)
            {
                case InventoryTypes.CurrentDeck:
                    UIManager.Instance.OpenInventory(GameManager.Instance.PersistentGameplayData.CurrentCardsList,"Current Cards");
                    break;
                case InventoryTypes.DrawPile:
                    UIManager.Instance.OpenInventory(CollectionManager.Instance.DrawPile,"Draw Pile");
                    break;
                case InventoryTypes.DiscardPile:
                    UIManager.Instance.OpenInventory(CollectionManager.Instance.DiscardPile,"Discard Pile");
                    break;
                case InventoryTypes.ExhaustPile:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }
}