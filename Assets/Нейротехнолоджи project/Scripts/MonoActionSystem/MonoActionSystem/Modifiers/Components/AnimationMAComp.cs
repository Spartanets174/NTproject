using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AnimationMAComp : MonoActionComponent
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
            OnSetup?.Invoke(this);
            currentGenderMode = genderMode;
            currentsportType = sportType;
            scenarioController = FindObjectOfType<ScenarioController>();

            SetCurrentCharacter();

            currentCharacter.OnAnimationEnd += onAnimationEndedInvoke;
            currentCharacter.StartAnimation();
        }
        else
        {
            CompleteComponent();
        }
    }

    private void SetCurrentCharacter()
    {
        Character character;
        if (currentGenderMode == GenderMode.Man)
        {
            character = scenarioController.ManPrefabs.Find(x => x.AnimationType == AnimationType.Trauma && x.SportType==currentsportType);
        }
        else
        {
            character = scenarioController.WomanPrefabs.Find(x => x.AnimationType == AnimationType.Trauma && x.SportType == currentsportType);
        }
        currentCharacter = Instantiate(character, parentToSpawn);
        currentCharacter.transform.localPosition = Vector3.zero;
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
        Destroy(currentCharacter.gameObject);
    }
}
