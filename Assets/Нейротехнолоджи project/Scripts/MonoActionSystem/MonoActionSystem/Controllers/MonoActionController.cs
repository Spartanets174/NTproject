using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///     Хранит список групп экшонов
///     Запускает необходимую группу экшонов
/// </summary>
public class MonoActionController : MonoBehaviour
{
  
    [Header("Список доступных ядер")]
    [SerializeField] protected List<MonoActionGroup> _monoActionGroups = new List<MonoActionGroup>();

    protected MonoActionGroup _selectedMonoActionGroup;
    /// <summary>
    ///     Возвращает текущую выбранную группу действий
    /// </summary>
    public MonoActionGroup selectedMonoActionGroup => _selectedMonoActionGroup;


    private bool isGroupSelected;
    public bool IsGroupSelected => isGroupSelected;


    /// <summary>
    ///     События вызываемые при выборе группы
    /// </summary>
    public event Action<MonoActionGroup> OnGroupSelected = null;

    /// <summary>
    ///     События вызываемые при запуске группы
    /// </summary>
    public event Action OnGroupStarted = null;

    /// <summary>
    ///     События вызываемые при окончании группы
    /// </summary>
    public event Action OnGroupEnded = null;

    /// <summary>
    ///     События вызываемые при изменении содержимого группы
    /// </summary>
    public event Action<MonoActionGroup> OnGroupBodyChanged = null;

    
    protected ScenarioMode selectedScenarioMode;
    public ScenarioMode SelectedScenarioMode => selectedScenarioMode;

    protected GenderMode selectedGenderMode;
    public GenderMode SelectedGenderMode => selectedGenderMode;

    public virtual void SelectGroup(int groupIndex)
    {
        if (groupIndex < 0 || groupIndex > _monoActionGroups.Count)
        {
            isGroupSelected = false;
            Debug.LogError($"Была обнаружена попытка передать индекс за пределами массива!");
            return;
        }

        _selectedMonoActionGroup = _monoActionGroups[groupIndex];
        OnGroupSelected?.Invoke(_selectedMonoActionGroup);
        isGroupSelected = true;
    }

    public virtual void StartSelectedGroup()
    {
        if (_selectedMonoActionGroup != null)
        {
            _selectedMonoActionGroup.OnGroupEnded += OnGroupEndedHandler;

            _selectedMonoActionGroup.StartGroup(selectedScenarioMode,selectedGenderMode);

            PerformOnGroupStartEvent();
        }
        else
        {
            Debug.LogError( $"Группа действий не выбрана!!!");
        }
    }

    protected virtual void OnGroupEndedHandler()
    {
        _selectedMonoActionGroup.OnGroupEnded -= OnGroupEndedHandler;

        PerformOnGroupEndedEvent();
    }


    public void PerformOnGroupStartEvent()
    {
        OnGroupStarted?.Invoke();
    }

    public void PerformOnGroupEndedEvent()
    {
        OnGroupEnded?.Invoke();
    }
}

public enum ScenarioMode { Training, Exam };
public enum GenderMode { Man, Woman };

public enum AnimationType { Success, Failure, Trauma };

public enum SportType { Run, KettlebellSnatch, Basketball, Tennis, Workout };
public enum SportMode { Inside, Outside };