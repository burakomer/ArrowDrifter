using System.Collections;
using System.Collections.Generic;
using TouchDevUltimate.Gameplay.Character;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    public Animator[] animators;

    //private LTSeq tweenSequence;

    private void Start()
    {
        animators = gameObject.GetComponentsInChildren<Animator>();

        foreach (Animator anim in animators)
        {
            anim.logWarnings = false;
        }
    }

    #region Animator Handlers
    public void SetBool(string name, bool value)
    {
        foreach (Animator anim in animators)
        {
            anim.SetBool(name, value);
        }
    }

    public void SetFloat(string name, float value)
    {
        foreach (Animator anim in animators)
        {
            anim.SetFloat(name, value);
        }
    }

    public void SetTrigger(string name)
    {
        foreach (Animator anim in animators)
        {
            anim.SetTrigger(name);
        }
    }

    public void ResetTrigger(string name)
    {
        foreach (Animator anim in animators)
        {
            anim.ResetTrigger(name);
        }
    }
    #endregion

}
