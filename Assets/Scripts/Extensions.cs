using System;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void ScaleTween(this GameObject obj, Vector3 scale, float time, LeanTweenType easeType)
    {
        LeanTween.cancel(obj);
        obj.LeanScale(scale, time).setEase(easeType);
    }
}
