using UnityEngine;

namespace NueGames.NueDeck.ThirdParty.NueTooltip.Core
{
    public class TooltipController : MonoBehaviour
    {

        [SerializeField] private RectTransform canvasRectTransform;
        
        private RectTransform _rectTransform;
        private Vector2 _followPos = Vector2.zero;
        private bool _isFollowEnabled;
        private Camera _cachedCamera;
        private Camera _followCam;
        private Transform _lastStaticTarget;
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        
        public void SetFollowPos(Transform staticTargetTransform = null,Camera cam =null)
        {
            if (staticTargetTransform)
            {
                var mainCam = cam;
                if (mainCam == null)
                {
                    if (!_cachedCamera)
                        _cachedCamera = Camera.main;
                    
                    mainCam = _cachedCamera;
                }

                if (mainCam == null)
                {
                    SetFollowPos();
                    return;
                }
                else
                {
                    _followCam = mainCam;
                }
                _lastStaticTarget = staticTargetTransform;
                _isFollowEnabled = false;
            }
            else
            {
                _followCam = null;
                _lastStaticTarget = null;
                _isFollowEnabled = true;
            }
        }


        private void Update()
        {
            SetPosition();
        }

        private void SetPosition()
        {
            if (_isFollowEnabled)
                _followPos = Input.mousePosition;
            else
            {
                if (_followCam && _lastStaticTarget)
                {
                    _followPos = _followCam.WorldToScreenPoint(_lastStaticTarget.position);
                }
            }

            var anchoredPos = _followPos / canvasRectTransform.localScale.x;
            
            if (anchoredPos.x + _rectTransform.rect.width>canvasRectTransform.rect.width)
                anchoredPos.x = canvasRectTransform.rect.width - _rectTransform.rect.width;
            
            if (anchoredPos.y + _rectTransform.rect.height>canvasRectTransform.rect.height)
                anchoredPos.y = canvasRectTransform.rect.height - _rectTransform.rect.height;
            
            _rectTransform.anchoredPosition = anchoredPos;
        }
    }
}