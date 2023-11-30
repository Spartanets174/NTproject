using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWrongAnimationMAComp : MonoActionComponent
{
    [SerializeField]
    private Transform parentToSpawn;

    public event Action OnAnimationEnded;
    public override void SetupComponent(GenderMode genderMode, SportType sportType)
    {
        if (IsAllowedToActivate)
        {
            _isComponentActive = true;
            OnSetupEvent?.Invoke();
            OnSetup?.Invoke();
            currentGenderMode = genderMode;
            currentsportType = sportType;
            scenarioController = FindObjectOfType<ScenarioController>();
            
            if (scenarioController.isRightExercise)
            {
                SetCurrentCharacter(AnimationType.Success);
            }
            else
            {
                SetCurrentCharacter(AnimationType.Failure);             
            }
            currentCharacter.OnAnimationEnd += onAnimationEndedInvoke;
            currentCharacter.StartAnimation();
        }
        else
        {
            CompleteComponent();
        }

    }

    private void SetCurrentCharacter(AnimationType animationType)
    {
        Character character;
        if (currentGenderMode == GenderMode.Man)
        {
            character = scenarioController.ManPrefabs.Find(x => x.AnimationType == animationType && x.SportType == currentsportType);
        }
        else
        {
            character = scenarioController.WomanPrefabs.Find(x => x.AnimationType == animationType && x.SportType == currentsportType);
        }
        currentCharacter=Instantiate(character, parentToSpawn);
        
        currentCharacter.transform.localPosition = Vector3.zero;
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
        Destroy(currentCharacter.gameObject);
    }
}
