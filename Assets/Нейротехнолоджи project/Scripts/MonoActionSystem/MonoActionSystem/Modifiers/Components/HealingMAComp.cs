using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class HealingMAComp : MonoActionComponent
{

    [SerializeField]
    private List<BodyParts> bodyParts;
    public List<BodyParts> BodyParts => bodyParts;

    private ScenarioController ScenarioController;
    public override void SetupComponent(Character character)
    {
        ScenarioController = FindFirstObjectByType<ScenarioController>();
        ScenarioController.IsHealingProcess = true;
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
        ScenarioController.IsHealingProcess = false;
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

public enum BodyParts
{
    None,
    a,
    b,
    c
}