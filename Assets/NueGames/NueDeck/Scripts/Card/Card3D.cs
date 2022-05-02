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
        
        private readonly Vector2 _dissolveOffset = new Vector2(0.1f, 0);
        private readonly Vector2 _dissolveSpeed = new Vector2(2f, 2f);

        
        public override void SetCard(CardData targetProfile,bool isPlayable)
        {
            base.SetCard(targetProfile,isPlayable);
           
            if (canvas)
                canvas.worldCamera = CollectionManager.Instance.HandController.cam;
        }


        public override void SetInactiveMaterialState(bool isInactive, Material inactiveMaterial = null)
        {
            base.SetInactiveMaterialState(isInactive, inactiveMaterial);
        }
    }
}