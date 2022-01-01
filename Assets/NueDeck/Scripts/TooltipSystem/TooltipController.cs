using UnityEngine;

namespace NueDeck.Scripts.TooltipSystem
{
    public class TooltipController : MonoBehaviour
    {

        private RectTransform _rectTransform;
        private Vector2 _followPos = Vector2.zero;
        private bool _isFollowEnabled;
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void SetFollowPos(Transform staticTargetTransform = null)
        {
            if (staticTargetTransform)
            {
                _followPos = staticTargetTransform.position;
               
                _isFollowEnabled = false;
            }
            else
            {
                _isFollowEnabled = true;
            }
        }


        private void Update()
        {
            SetPosition();
            PreserveRect();
        }

        private void PreserveRect()
        {
            var pivotX = _followPos.x / Screen.width;
            var pivotY = _followPos.y / Screen.height;

            _rectTransform.pivot = new Vector2(pivotX, pivotY);
        }

        private void SetPosition()
        {
            if (_isFollowEnabled)
                _followPos = Input.mousePosition;
            
            transform.position = _followPos;
        }
    }
}