using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Character : MonoBehaviour
{

    [SerializeField]
    private AnimationType m_animationType;
    public AnimationType AnimationType => m_animationType;

    [SerializeField]
    private SportType m_sportType;
    public SportType SportType => m_sportType;

    

    public event Action OnAnimationStart;
    public event Action OnAnimationEnd;


    public void StartAnimation()
    {
        OnAnimationStart?.Invoke();
    }
    public void EndAnimation()
    {
        OnAnimationEnd?.Invoke();
    }  
}
