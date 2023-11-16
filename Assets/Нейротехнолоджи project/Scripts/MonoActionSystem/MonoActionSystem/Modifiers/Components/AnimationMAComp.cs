using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMAComp : MonoActionComponent
{
    public override void SetupComponent()
    {
        if (IsAllowedToActivate)
        {

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
