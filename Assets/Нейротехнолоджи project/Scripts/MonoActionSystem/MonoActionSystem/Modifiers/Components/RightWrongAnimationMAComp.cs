using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWrongAnimationMAComp : MonoActionComponent
{
    [SerializeField]
    private string animationWrongClip;
    [SerializeField]
    private string animationRightClip;

    public event Action OnAnimationEnded;
    public override void SetupComponent(Character character)
    {
        if (IsAllowedToActivate)
        {
            _isComponentActive = true;
            OnSetupEvent?.Invoke();
            OnSetup?.Invoke();
            currentCharacter = character;

            currentCharacter.OnAnimationEnd += onAnimationEndedInvoke;
            if (currentCharacter.isRightExercise)
            {
                currentCharacter.StartAnimation(animationRightClip);
            }
            else
            {
                currentCharacter.StartAnimation(animationWrongClip);
            }
           
        }
        else
        {
            CompleteComponent();
        }

    }
    private void onAnimationEndedInvoke()
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
