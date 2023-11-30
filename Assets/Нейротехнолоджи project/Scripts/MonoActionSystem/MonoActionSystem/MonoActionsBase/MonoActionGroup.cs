
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
///     Итерирует и отслеживает состояние каждого ядра в группе
///     Что-то по типу Сценария
/// </summary>
public class MonoActionGroup : MonoBehaviour
{
    protected List<MonoActionCore> _actionCores = new List<MonoActionCore>();
    public List<MonoActionCore> ActionCores => _actionCores;


    protected MonoActionCore _currentCoreInAction;
    public MonoActionCore CurrentCoreInAction => _currentCoreInAction;


    protected int _currentCoreIndex;
    /// <summary>
    ///     Возвращает индекс текущего активного ядра
    /// </summary>
    public int currentCoreIndex => _currentCoreIndex;


    public event Action OnGroupStarted = null;
    public event Action OnGroupEnded = null;
    
    public event Action OnCoreSetup = null;
    public event Action OnCorePreSetup = null;
    public event Action OnCoreCompleted = null;


    protected ScenarioMode selectedScenarioMode;
    public ScenarioMode SelectedScenarioMode => selectedScenarioMode;

    protected GenderMode selectedGenderMode;
    public GenderMode SelectedGenderMode => selectedGenderMode;


    [ContextMenu("Get Children Cores")]
    private void GetChildrenCores()
    {
        _actionCores.Clear();
        List<MonoActionCore> actionCores = GetComponentsInChildren<MonoActionCore>().ToList();
        foreach (MonoActionCore core in actionCores)
        {
            if (core.ActivationScenarioMode == SelectedScenarioMode||core.AlwaysActivate)
            {
                _actionCores.Add(core); 
            }
        }
    }

    public void StartGroup(ScenarioMode scenarioMode, GenderMode genderMode)
    { 
        ResetGroup();

        selectedScenarioMode = scenarioMode;
        selectedGenderMode = genderMode;

        PerformOnGroupStartedEvent();

        GetChildrenCores();
        SetupNextCore();
    }
    private void OnAllComponentsEndedWork()
    {
        PerformOnCoreCompletedEvent();

        SetupNextCore();
    }

    public virtual void SetupNextCore()
    {
        if (_currentCoreInAction != null)
        {
            _currentCoreInAction.OnAllComponentsEndedWork -= OnAllComponentsEndedWork;
        }

        _currentCoreIndex++;

        if (_currentCoreIndex >= _actionCores.Count)
        {
            PerformOnGroupEndedEvent();
        }
        else
        {
            _currentCoreInAction = _actionCores[_currentCoreIndex];
            _currentCoreInAction.OnAllComponentsEndedWork += OnAllComponentsEndedWork;

            PerformOnCorePreSetupEvent();

            _currentCoreInAction.SetupCore(selectedScenarioMode, selectedGenderMode);

            PerformOnCoreSetupEvent();
        }
    }

    public void ResetGroup()
    {
        _currentCoreIndex = -1;
        for (int i = 0; i < _actionCores.Count; i++)
        {
            _actionCores[i].OnAllComponentsEndedWork -= OnAllComponentsEndedWork;
            _actionCores[i].ResetCore();
        }
    }



    protected void PerformOnGroupStartedEvent()
    {
        OnGroupStarted?.Invoke();
    }
    protected void PerformOnGroupEndedEvent()
    {
        OnGroupEnded?.Invoke();
    }

    protected void PerformOnCoreSetupEvent()
    {
        OnCoreSetup?.Invoke();
    }

    protected void PerformOnCorePreSetupEvent()
    {
        OnCorePreSetup?.Invoke();
    }

    protected void PerformOnCoreCompletedEvent()
    {
        OnCoreCompleted?.Invoke();
    }
}