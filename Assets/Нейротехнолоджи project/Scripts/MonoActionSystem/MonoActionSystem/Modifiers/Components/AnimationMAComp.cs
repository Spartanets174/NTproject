using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMAComp : MonoActionComponent
{
    [SerializeField]
    private string animationClip;
  
    public event Action OnAnimationEnded;
    public override void SetupComponent(Character character)
    {
        if (IsAllowedToActivate)
        {
            _isComponentActive = true;
            OnSetupEvent?.Invoke();
            OnSetup?.Invoke();
            currentCharacter = character;

            character.OnAnimationEnd += onAnimationEndedInvoke;
            character.StartAnimation(animationClip);

        }
        else
        {
            CompleteComponent();
        }

    }

    private void  onAnimationEndedInvoke()
    {
        OnAnimationEnded?.Invoke();
    }

    public override void CompleteComponent()
    {
        LocalReset();
        base.CompleteComponent();
    }

    public override void ResetComponent()
    {
        LocalReset();
        base.ResetComponent();
    }

    public override void SkipComponent()
    {
        LocalReset();
        base.SkipComponent();
    }

    private void LocalReset()
    {

    }
}
