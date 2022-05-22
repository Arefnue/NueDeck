using System;
using NueGames.NueDeck.ThirdParty.NueTooltip.CursorSystem;
using UnityEngine;

namespace NueGames.NueDeck.ThirdParty.NueTooltip.Core
{
    public class OnCursorChangedEventArgs : EventArgs
    {
        public CursorType CursorType;

        public OnCursorChangedEventArgs(CursorType targetType)
        {
            CursorType = targetType;
        }
    }
    public class CursorController : MonoBehaviour
    {
        [Header("Cursor")] 
        [SerializeField] private CursorData cursorData;
        
        private CursorType _activeCursorType = CursorType.Default;
        private CursorProfile _activeCursorProfile;
        private float _currentFrameTimer = 0f;
        private int _currentAnimationFrame = 0;
        private int _totalAnimationFrame;
        private int _totalAnimationClickedFrame;
        private bool _canAnimate;
        public event EventHandler<OnCursorChangedEventArgs> OnCursorChanged;
        private bool _isClicked;
        
        private void Awake()
        {
            SetActiveCursor(CursorType.Default);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _currentFrameTimer = 0;
                _isClicked = true;
                _currentAnimationFrame = 0;
                ChangeFrame(_isClicked);
            }
            if (Input.GetMouseButtonUp(0))
            {
                _currentFrameTimer = 0;
                _isClicked = false;
                _currentAnimationFrame = 0;
                ChangeFrame(_isClicked);
            }
            
            if (_canAnimate)
            {
                _currentFrameTimer -= Time.deltaTime;
                if (_currentFrameTimer<=0f)
                {
                    _currentFrameTimer += _activeCursorProfile.frameRate;
                    _currentAnimationFrame = (_currentAnimationFrame + 1) % (_totalAnimationFrame<=0 ? 1 : _totalAnimationFrame);

                    if (_isClicked)
                        if (_activeCursorProfile.cursorAnimationClickedFrameList.Count > 0)
                            _currentAnimationFrame = (_currentAnimationFrame + 1) % (_totalAnimationClickedFrame<=0 ? 1 : _totalAnimationClickedFrame);
                    
                    ChangeFrame(_isClicked);
                }
            }
        }

       
        public void SetActiveCursor(CursorType targetType,int startFrameIndex =0)
        {
            _activeCursorType = targetType;
            _activeCursorProfile = cursorData.GetCursor(_activeCursorType);
            
            _canAnimate = false;
            if (_activeCursorProfile != null)
            {
                _currentAnimationFrame = startFrameIndex;
                _totalAnimationFrame = _activeCursorProfile.cursorAnimationFrameList.Count;
                _totalAnimationClickedFrame = _activeCursorProfile.cursorAnimationClickedFrameList.Count;
                
                if (_activeCursorProfile.useCursorAnimation)
                {
                    _currentFrameTimer = _activeCursorProfile.frameRate;
                    _canAnimate = true;
                }
                else
                {
                   ChangeFrame();
                }
            }
            else
            {
                Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);
            }
            OnCursorChanged?.Invoke(this,new OnCursorChangedEventArgs(targetType));
        }
        
        
         private void ChangeFrame(bool isClicked = false)
         {
             if (_activeCursorProfile == null) return;
             if (_activeCursorProfile.cursorAnimationFrameList.Count <= _currentAnimationFrame) return;
             
             var animationFrame = _activeCursorProfile.cursorAnimationFrameList[_currentAnimationFrame];

             if (isClicked)
                 if (_activeCursorProfile.cursorAnimationClickedFrameList.Count > 0)
                     animationFrame = _activeCursorProfile.cursorAnimationClickedFrameList[_currentAnimationFrame];
                
             
             
             if (animationFrame != null)
                 Cursor.SetCursor(animationFrame.cursorTexture, animationFrame.cursorOffset, CursorMode.Auto);
             else
                 Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);
         }


    }
}
