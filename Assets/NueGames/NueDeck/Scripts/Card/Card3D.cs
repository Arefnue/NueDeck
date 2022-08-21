using System.Collections;
using NueGames.NueDeck.Scripts.Data.Collection;
using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Card
{
    public class Card3D : CardBase
    {
        [Header("3D Settings")]
        [SerializeField] private Canvas canvas;
      
        public override void SetCard(CardData targetProfile,bool isPlayable)
        {
            base.SetCard(targetProfile,isPlayable);
           
            if (canvas)
                canvas.worldCamera = CollectionManager.HandController.cam;
        }
        
        public override void SetInactiveMaterialState(bool isInactive)
        {
            base.SetInactiveMaterialState(isInactive);
        }
    }
}