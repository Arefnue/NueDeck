using System;
using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Utils
{
    public class InventoryHelper : MonoBehaviour
    {
        [SerializeField] private InventoryTypes inventoryType;
        
        private UIManager UIManager => UIManager.Instance;
        
        public void OpenInventory()
        {
            switch (inventoryType)
            {
                case InventoryTypes.CurrentDeck:
                    UIManager.OpenInventory(GameManager.Instance.PersistentGameplayData.CurrentCardsList,"Current Cards");
                    break;
                case InventoryTypes.DrawPile:
                    UIManager.OpenInventory(CollectionManager.Instance.DrawPile,"Draw Pile");
                    break;
                case InventoryTypes.DiscardPile:
                    UIManager.OpenInventory(CollectionManager.Instance.DiscardPile,"Discard Pile");
                    break;
                case InventoryTypes.ExhaustPile:
                    UIManager.OpenInventory(CollectionManager.Instance.ExhaustPile,"Exhaust Pile");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }
}