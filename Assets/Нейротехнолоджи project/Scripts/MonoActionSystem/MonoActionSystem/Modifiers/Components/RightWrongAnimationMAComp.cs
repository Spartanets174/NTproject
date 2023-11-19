using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWrongAnimationMAComp : MonoActionComponent
{
    public override void SetupComponent(Character character)
    {
        if (IsAllowedToActivate)
        {
            _isComponentActive = true;
            OnSetupEvent?.Invoke();
            OnSetup?.Invoke();
            currentCharacter = character;
        }
        else
        {
            CompleteComponent();
        }

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
