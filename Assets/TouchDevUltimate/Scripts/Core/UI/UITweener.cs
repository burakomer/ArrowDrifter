using System;
using UnityEngine;

namespace TouchDevUltimate.Core.UI
{
    public class UITweener : MonoBehaviour
    {
        // TODO : Adapt this so it works for mostly every UI element.
        public enum TweenType
        {
            Move,
            MoveX,
            MoveY,
            Scale,
            ScaleX,
            ScaleY,
        }

        public TweenType tweenType;
        public Vector3 toVector;

        public void OnTween(bool entrance, float tweenSpeed, LeanTweenType easeType)
        {
            switch (tweenType)
            {
                case TweenType.Move:
                    gameObject.LeanMoveLocal(entrance ? transform.localPosition - toVector : transform.localPosition + toVector, tweenSpeed).setEase(easeType);
                    break;
                case TweenType.MoveX:
                    gameObject.LeanMoveLocalX(entrance ? transform.localPosition.x - toVector.x : transform.localPosition.x + toVector.x, tweenSpeed).setEase(easeType);
                    break;
                case TweenType.MoveY:
                    gameObject.LeanMoveLocalY(entrance ? transform.localPosition.y - toVector.y : transform.localPosition.y + toVector.y, tweenSpeed).setEase(easeType);
                    break;
                case TweenType.Scale:
                    gameObject.LeanScale(entrance ? transform.localScale - toVector : transform.localPosition + toVector, tweenSpeed).setEase(easeType);
                    break;
                case TweenType.ScaleX:
                    gameObject.LeanScaleX(entrance ? transform.localScale.x - toVector.x : transform.localScale.x + toVector.x, tweenSpeed).setEase(easeType);
                    break;
                case TweenType.ScaleY:
                    gameObject.LeanScaleY(entrance ? transform.localScale.y - toVector.y : transform.localScale.y + toVector.y, tweenSpeed).setEase(easeType);
                    break;
                default:
                    break;
            }
        }
    }
}
