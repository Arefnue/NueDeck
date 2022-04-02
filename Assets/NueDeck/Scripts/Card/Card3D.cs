using System.Collections;
using NueDeck.Scripts.Data.Collection;
using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.Card
{
    public class Card3D : CardBase
    {
        [Header("3D Settings")]
        [SerializeField] private MeshRenderer cardMeshRenderer;
        [SerializeField] private Canvas canvas;

        private Color _dissolveColor;
        private Color _baseColor;
        private Color _colorOutline;
        private Material _cardMaterial;
        private readonly Vector2 _dissolveOffset = new Vector2(0.1f, 0);
        private readonly Vector2 _dissolveSpeed = new Vector2(2f, 2f);

        
        public override void SetCard(CardData targetProfile,bool isPlayable)
        {
            base.SetCard(targetProfile,isPlayable);
            if (cardMeshRenderer)
            {
                _cardMaterial = cardMeshRenderer.material;
                _baseColor = _cardMaterial.GetColor("_Color");
                _colorOutline = _cardMaterial.GetColor("_OutlineColor");
                _dissolveColor = _cardMaterial.GetColor("_DissolveColor");
            }

            if (canvas)
                canvas.worldCamera = CollectionManager.Instance.HandController.cam;
        }


        public override void SetInactiveMaterialState(bool isInactive, Material inactiveMaterial = null)
        {
            base.SetInactiveMaterialState(isInactive, inactiveMaterial);
            if (cardMeshRenderer)
                cardMeshRenderer.sharedMaterial = isInactive ? inactiveMaterial : _cardMaterial;
        }
        
        private IEnumerator Dissolve()
        {
            var waitFrame = new WaitForEndOfFrame();
            Vector2 t = Vector2.zero - _dissolveOffset;
            while (t.x < 1) {
                t.x = (t.x + Time.deltaTime * _dissolveSpeed.x);
                if (t.y < 1) {
                    t.y = (t.y + Time.deltaTime * _dissolveSpeed.y);
                }

                if (cardMeshRenderer)
                {
                    _cardMaterial.SetVector("_Dissolve", t);
                    _cardMaterial.SetColor("_DissolveColor", _dissolveColor * 4 * t.y);
                }

                yield return waitFrame;
            }
            Destroy(gameObject);
        }
    }
}