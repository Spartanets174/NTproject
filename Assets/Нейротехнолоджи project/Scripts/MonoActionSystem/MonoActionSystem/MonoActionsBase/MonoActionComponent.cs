
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///     явл€етс€ частичкой €дра и содержит какое-то своЄ состо€ние
///     Ёто состо€ние затем передаЄтс€ на €дро дл€ отслеживани€ итогов выполнени€ действий компонента
///     ћожно использовать как часть интерактива внутри шага
/// </summary>
[System.Serializable]
public class MonoActionComponent : MonoBehaviour
{
    [Header("Mode")]
    [SerializeField] protected MonoActionController.ScenarioMode m_activationScenarioMode;
    public MonoActionController.ScenarioMode ActivationScenarioMode => m_activationScenarioMode;
    [SerializeField] protected bool m_alwaysActivate = true;
    public bool AlwaysActivate => m_alwaysActivate;

  
    [Header("Component Title")]
    [SerializeField] protected string m_componentName;


    public Action OnSetup = null;
    public Action OnComplete = null;

    public UnityEvent OnSetupEvent;
    public UnityEvent OnCompleteEvent;

    protected bool _isCompleted;
    public bool isCompleted => _isCompleted;


    protected bool _isComponentActive;
    public bool IsComponentActive => _isComponentActive;


    private bool isAllowedToActivate;
    public bool IsAllowedToActivate => isAllowedToActivate;

    protected MonoActionController.ScenarioMode currentScenarioMode;


    public virtual void SetRuntimeMode(MonoActionController.ScenarioMode _scenarioMode)
    {
        isAllowedToActivate = m_alwaysActivate || m_activationScenarioMode == _scenarioMode;
        currentScenarioMode = _scenarioMode;
    }

    public virtual void SetupComponent()
    {
        if (isAllowedToActivate)
        {        
            _isComponentActive = true;
            OnSetupEvent?.Invoke();
            OnSetup?.Invoke();
           
        }
        else
        {
            CompleteComponent();
        }
    }
    public virtual void CompleteComponent()
    {
        _isComponentActive = false;
        _isCompleted = true;

        OnCompleteEvent?.Invoke();
        OnComplete?.Invoke();
    }

    public virtual void ResetComponent()
    {     
        _isCompleted = false;
    }


    public virtual void SkipComponent()
    {
        if (isCompleted) return;
        _isCompleted = true;
        OnCompleteEvent?.Invoke();
        OnComplete?.Invoke();
    }

    public virtual void ForceFailComponent()
    {
        if (isCompleted) return;
        _isCompleted = true;
        OnCompleteEvent?.Invoke();
        OnComplete?.Invoke();
    }



    private IEnumerator DelayedCompletion()
    {
        yield return new WaitForSeconds(1f);
    }
}