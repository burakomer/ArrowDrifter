using System;
using System.Collections;
using TouchDevUltimate.Tools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TouchDevUltimate.Core.Input
{
    public class TouchGesture : TouchInput, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public float tapTime = 0.05f;
        public float swipeTime = 0.5f;
        public bool detectVertical;
        public bool detectHorizontal;

        private int fingerId;
        private bool gestureProcessing;
        private bool isSwiping;

        private bool swipeTimeElapsed;
        //private Vector2 startPos;
        //private Vector2 newPos;

        private Timer timer;

        private void Start()
        {
            //timer = new Timer();
            //timer.interval = swipeTime;
            //timer.Elapsed += OnElapsed;
        }

        private void OnElapsed()
        {
            swipeTimeElapsed = true;
        }

        private void Update()
        {
            //timer.Update();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!interactive)
            {
                return;
            }

            isSwiping = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!interactive)
            {
                return;
            }

            if (!swipeTimeElapsed)
            {
                Vector3 swipeVector = (eventData.position - eventData.pressPosition).normalized;
                Debug.Log(GetSwipeDirection(swipeVector).ToString());
                GameManager.Instance.InputManager.Gesture(name, GetSwipeDirection(swipeVector));
            }

            swipeTimeElapsed = false;
            isSwiping = false;
            gestureProcessing = false;
            //timer.Stop();
        }

        private GestureType GetSwipeDirection(Vector2 swipeVector)
        {
            float positiveX = Mathf.Abs(swipeVector.x);
            float positiveY = Mathf.Abs(swipeVector.y);
            GestureType swipeDir;

            if(detectHorizontal && !detectVertical)
            {
                swipeDir = (swipeVector.x > 0) ? GestureType.SwipeRight : GestureType.SwipeLeft;
            }
            else if (!detectHorizontal && detectVertical)
            {
                swipeDir = (swipeVector.y > 0) ? GestureType.SwipeUp : GestureType.SwipeDown;
            }
            else
            {
                if (positiveX > positiveY)
                {
                    swipeDir = (swipeVector.x > 0) ? GestureType.SwipeRight : GestureType.SwipeLeft;
                }
                else
                {
                    swipeDir = (swipeVector.y > 0) ? GestureType.SwipeUp : GestureType.SwipeDown;
                } 
            }
            //Debug.Log(swipeDir);
            return swipeDir;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!interactive)
            {
                return;
            }

            fingerId = eventData.pointerId;
            gestureProcessing = true;

            StartCoroutine(CheckGesture());
            //timer.Start();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(!isSwiping && gestureProcessing)
            {
                GameManager.Instance.InputManager.Gesture(name, GestureType.Tap);
            }

            gestureProcessing = false;
        }

        private IEnumerator CheckGesture()
        {
            yield return new WaitForSeconds(tapTime);

            OnPointerUp(null);

            yield return new WaitForSeconds(swipeTime - tapTime);

            if (isSwiping)
            {
                swipeTimeElapsed = true;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnBeginDrag(eventData);
        }
    }
}