using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField]
    private Animator animationCharacter;

    public event Action OnAnimationStart;
    public event Action OnAnimationEnd;

    public void StartTraumaAnimation()
    {
        animationCharacter.SetBool("isTraumaAnimation", true);
        OnAnimationStart?.Invoke();
    }
    public void StartSuccessfulAnimation()
    {
        animationCharacter.SetBool("isSuccessfulAnimation", true);
        OnAnimationStart?.Invoke();
    }
    public void StartFailAnimation()
    {
        animationCharacter.SetBool("isFailAnimation",true);
        OnAnimationStart?.Invoke();
    }

    public void EndTraumaAnimation()
    {
        animationCharacter.SetBool("isTraumaAnimation", false);
        OnAnimationEnd?.Invoke();
    }
    public void EndSuccessfulAnimation()
    {
        animationCharacter.SetBool("isSuccessfulAnimation", false);
        OnAnimationEnd?.Invoke();
    }
    public void EndFailAnimation()
    {
        animationCharacter.SetBool("isFailAnimation", false);
        OnAnimationEnd?.Invoke();
    }
}
