
using System;
using System.Collections;
using UnityEngine;

/// <summary>
///     Будет обрабатывать текущий шаг и текущее действие
///     Если мы проходим сценарий - отображаем шаги и текущий шаг
///     Если мы сталкиваемся с каким-то специфичным шагом - мы отображаем инфу о нём
/// </summary>
public class ScenarioController : MonoActionController, IBootstrapper
{
    public event Action OnStepSetup = null;
    public event Action OnStepCompleted = null;
    public event Action OnScenarioStarted = null;
    public event Action OnScenarioEnded = null;


    private int currentStepIndex;
    public int CurrentStepIndex => currentStepIndex;

    private bool m_isInitialized;
    public bool IsInitialized => m_isInitialized;


    public void Init()
    {
        if (m_isInitialized) return;

        m_isInitialized = true;
    }

    private void OnDestroy()
    {
        if (selectedMonoActionGroup != null)
        {
            selectedMonoActionGroup.OnCoreSetup -= StepStartedHandler;
            selectedMonoActionGroup.OnCoreCompleted -= StepCompletedHandler;
        }
    }


    [ContextMenu("Начать сценарий")]
    public void StartScenario(ScenarioMode scenarioMode)
    {

        SelectGroup(0);

        selectedScenarioMode = scenarioMode;


        selectedMonoActionGroup.OnCoreSetup += StepStartedHandler;
        selectedMonoActionGroup.OnCoreCompleted += StepCompletedHandler;

        StartSelectedGroup();
        OnScenarioStarted?.Invoke();
    }

    [ContextMenu("Закончить сценарий")]
    public void EndScenario()
    {
        if (_selectedMonoActionGroup != null)
        {
            selectedMonoActionGroup.OnCoreSetup -= StepStartedHandler;
            selectedMonoActionGroup.OnCoreCompleted -= StepCompletedHandler;

            currentStepIndex = 0;

            _selectedMonoActionGroup = null;
        }

        OnScenarioEnded?.Invoke();
    }

    public override void StartSelectedGroup()
    {
        if (_selectedMonoActionGroup != null)
        {
            _selectedMonoActionGroup.OnGroupEnded += OnGroupEndedHandler;

            _selectedMonoActionGroup.StartGroup(selectedScenarioMode, selectedGenderMode); ;

            PerformOnGroupStartEvent();
        }
        else
        {
            Debug.LogError($"Группа действий не выбрана!!!");
        }
    }


    protected override void OnGroupEndedHandler()
    {

        base.OnGroupEndedHandler();

        if (selectedScenarioMode == ScenarioMode.Exam)
        {
           

            float maxScores = 0;
            float currentScores = 0;

           /* foreach (RepairableReportData data in controller.RepairableReportDatas)
            {
                foreach (var item in data.observers)
                {
                    maxScores++;
                    if (item.CurrentRepairIndex == item.RepairIndex)
                    {
                        currentScores++;
                    }
                }
            }
            foreach (DefectoscopyReportData data in controller.DefectoscopyReportDatas)
            {
                foreach (var item in data.DefectDatas)
                {
                    maxScores++;
                    if (item.isCorrect)
                    {
                        currentScores++;
                    }
                }
            }
            plStudySender.SendResult(currentScores, maxScores);*/
        }
    }


    private void StepStartedHandler()
    {
        OnStepSetup?.Invoke();
    }

    private void StepCompletedHandler()
    {
        currentStepIndex++;
        OnStepCompleted?.Invoke();
    }

  
}
