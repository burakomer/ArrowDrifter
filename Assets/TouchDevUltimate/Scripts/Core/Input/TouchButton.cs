using UnityEngine;
using UnityEngine.EventSystems;
using TouchDevUltimate.Core.UI;
using UnityEngine.Events;

namespace TouchDevUltimate.Core.Input
{
    public class TouchButton : TouchInput, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("UI Button")]
        public bool isUIButton;
        [ConditionalHide("isUIButton", true)] public NavigationType navType;
        [ConditionalHide("isUIButton", true)] public bool mustActOnPeek;
        public UnityEvent OnUIButton;

        [Header("Tween Properties")] 
        public bool tweening = true;
        
        [Header("Pressed")]
        [ConditionalHide("tweening", true)] public LeanTweenType onPressTweenType;
        [ConditionalHide("tweening", true)] public float onPressScale;
        [ConditionalHide("tweening", true)] public float onPressScaleTime;
        
        [Header("Released")]
        [ConditionalHide("tweening", true)] public LeanTweenType onReleaseTweenType;
        [ConditionalHide("tweening", true)] public float onReleaseScaleTime;

        private int fingerId;
        private bool touchExists;
        private bool fingerRemoved;
        
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            SetInteractive(true);
        }

        /// <param name="pointerId">Sets the initial pointer Id for future checking</param>
        private void ButtonPressed(int pointerId)
        {
            // Tween pressed
            TweenButton(Vector3.one * onPressScale, onPressScaleTime, onPressTweenType);

            touchExists = true;
            fingerId = pointerId;

            if (!isUIButton)
            {
                GameManager.Instance.InputManager.Button(name, InputState.Pressed);
            }
        }

        private void ButtonReleased(bool releasedInside)
        {
            touchExists = false;
            fingerRemoved = false;
            fingerId = -1;

            if (releasedInside)
            {
                // Tween released
                TweenButton(Vector3.one, onReleaseScaleTime, onReleaseTweenType);

                if (isUIButton)
                {
                    OnUIButton?.Invoke();

                    switch (navType)
                    {
                        case NavigationType.Push:
                            NavigationManager.Instance.Push(name);
                            break;
                        case NavigationType.Pop:
                            NavigationManager.Instance.Pop();
                            break;
                        case NavigationType.Peek:
                            NavigationManager.Instance.Peek(name, mustActOnPeek);
                            break;
                        case NavigationType.PeekPop:
                            NavigationManager.Instance.Pop(name, mustActOnPeek);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    GameManager.Instance.InputManager?.Button(name, InputState.Up);
                }
            }
        }

        private void FingerRemoved(bool removed)
        {
            fingerRemoved = removed;
            
            if (removed)
            {
                // Tween released
                TweenButton(Vector3.one, onPressScaleTime, onPressTweenType);
            }
            else
            {
                // Tween pressed
                TweenButton(Vector3.one * onPressScale, onPressScaleTime, onPressTweenType);
            }
        }

        #region Pointer Events

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!interactive || touchExists)
            {
                return;
            }

            ButtonPressed(eventData.pointerId);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!interactive || !(fingerId == eventData.pointerId))
            {
                return;
            }

            if (fingerRemoved)
            {
                ButtonReleased(false);
            }
            else
            {
                ButtonReleased(true);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!interactive || !(fingerId == eventData.pointerId) || !touchExists)
            {
                return;
            }

            FingerRemoved(false);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!interactive || !(fingerId == eventData.pointerId) || !touchExists)
            {
                return;
            }

            FingerRemoved(true);
        }
        #endregion

        public void TweenButton(Vector3 scale, float scaleTime, LeanTweenType tweenType)
        {
            if (tweening)
            {
                LeanTween.cancel(gameObject);
                LeanTween.scale(_rectTransform, scale, scaleTime).setEase(tweenType);
            }
        }
    }
}