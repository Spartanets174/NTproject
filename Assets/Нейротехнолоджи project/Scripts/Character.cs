using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField]
    private Animator animationCharacter;

    public bool isRightExercise { get; set; } = false;

    public event Action OnAnimationStart;
    public event Action OnAnimationEnd;

    private string lastPlayableAnim;

    public void StartAnimation(string anim)
    {
        lastPlayableAnim = anim;
        animationCharacter.SetBool(anim, true);
        OnAnimationStart?.Invoke();
    }
    public void EndAnimation()
    {
        animationCharacter.SetBool(lastPlayableAnim, false);
        OnAnimationEnd?.Invoke();
    }  
}
