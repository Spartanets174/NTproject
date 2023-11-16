
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
    [Header("Characters")]
    [SerializeField]
    private Character womanPrefab;
    [SerializeField]
    private Character manPrefab;


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
    public event Action OnCoreCompleted = null;


    protected MonoActionController.ScenarioMode selectedScenarioMode;
    public MonoActionController.ScenarioMode SelectedScenarioMode => selectedScenarioMode;

    protected MonoActionController.GenderMode selectedGenderMode;
    public MonoActionController.GenderMode SelectedGenderMode => selectedGenderMode;

    private Character currentCharacterPrefab;


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

    public void StartGroup(MonoActionController.ScenarioMode scenarioMode, MonoActionController.GenderMode genderMode)
    { 
        ResetGroup();

        selectedScenarioMode = scenarioMode;
        selectedGenderMode = genderMode;

        currentCharacterPrefab = selectedGenderMode == MonoActionController.GenderMode.Man ? manPrefab : womanPrefab;
        PerformOnGroupStartedEvent();

        GetChildrenCores();
        SetupNextCore();
    }
    private void OnAllComponentsEndedWork()
    {
        PerformOnCoreCompletedEvent();

        SetupNextCore();
    }

    public void SetupNextCore()
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
            _currentCoreInAction.SetupCore(selectedScenarioMode, currentCharacterPrefab);

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
    protected void PerformOnCoreCompletedEvent()
    {
        OnCoreCompleted?.Invoke();
    }
}