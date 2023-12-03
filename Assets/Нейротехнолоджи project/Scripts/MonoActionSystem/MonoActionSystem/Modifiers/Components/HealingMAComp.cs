using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class HealingMAComp : MonoActionComponent
{

    [SerializeField]
    private List<BodyParts> bodyParts;
    public List<BodyParts> BodyParts => bodyParts;

    public override void SetupComponent(GenderMode genderMode, SportType sportType)
    {
       
        if (IsAllowedToActivate)
        {
            _isComponentActive = true;
            OnSetupEvent?.Invoke();
            OnSetup?.Invoke(this);
            currentGenderMode = genderMode;
            currentsportType = sportType;
            scenarioController = FindObjectOfType<ScenarioController>();
            scenarioController.IsHealingProcess = true;
        }
        else
        {
            CompleteComponent();
        }

    }

    public override void CompleteComponent()
    {
        LocalReset();
        scenarioController.IsHealingProcess = false;
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
    LeftAnkleJoint,
    RightAnkleJoint,
    LeftKnee,
    RightKnee,
    LeftShoulder,
    RightShoulder,
    LeftElbow,
    RightElbow,
    LeftWrist,
    RightWrist,
    SmallBack
}
public enum Instrument
{
    Cordus,
    Sacrus
}