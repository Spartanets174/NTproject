/*using PLCore.PLUnity.MVPReactive;
using PLCore.PLUnity.Utils;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UniRx;
using UnityEngine;

public class ScenarioControllerPresenter : PresenterBehaviour<ScenarioController>, IBootstrapper
{
    [SerializeField]
    private ScenarioStepViewPresenter stepViewPresenterPrefab;

    [SerializeField]
    private Transform m_parent;


    [MVPReactiveProperty]
    public ReactiveProperty<bool> IsScenarioInProgress = new ReactiveProperty<bool>();

    [MVPReactiveProperty]
    public ReactiveProperty<bool> IsScenarioInProgressReversed = new ReactiveProperty<bool>();

    [MVPReactiveProperty]
    public ReactiveProperty<string> ScenarioTitleText = new ReactiveProperty<string>();

    [MVPReactiveProperty]
    public ReactiveProperty<string> ScenarioDescriptionText = new ReactiveProperty<string>();


    private bool m_isInitialized = false;
    public bool IsInitialized => m_isInitialized;


    private ScenarioMAGroup selectedScenarioGroup;
    private List<ScenarioStepViewPresenter> scenarioStepViewPresenters = new List<ScenarioStepViewPresenter>();


    private ScenarioMACore currentCore;
    private int componentStepIndex = 0;

    private ScenarioController scenarioController;

    protected override void OnDestroy()
    {
        // ...
        model.OnGroupStarted -= OnScenarioStarted;
        model.OnGroupEnded -= OnScenarioEnded;

        model.OnStepSetup -= CoreOnStepSetup;
        model.OnStepCompleted -= CoreOnStepCompleted;

        RemoveModel();
        foreach (var pres in scenarioStepViewPresenters)
        {
            Destroy(pres.gameObject);
        }
        base.OnDestroy();
    }

    [ContextMenu("Find Model")]
    public void InitializeBootstrapper()
    {
        scenarioController = SimpleObjectFinder.TryFindComponentAtScene<ScenarioController>();
        InjectModel(scenarioController);
    }


    protected override void OnInjectModel(ScenarioController model)
    {
        base.OnInjectModel(model);

        Debug.Log($"Произошла инъекция ScenarioPresenter");

        // ...
        model.OnGroupStarted += OnScenarioStarted;
        model.OnGroupEnded += OnScenarioEnded;

        model.OnStepSetup += CoreOnStepSetup;
        model.OnStepCompleted += CoreOnStepCompleted;
    }

    private void OnScenarioStarted(MonoActionGroupSetupData setupData)
    {
        if (scenarioController.SelectedScenarioMode == MonoActionController.ScenarioMode.Training)
        {
            IsScenarioInProgress.Value = true;
        }
        
        IsScenarioInProgressReversed.Value = false;

        scenarioStepViewPresenters.Clear();
        componentStepIndex = 0;

        if (model.selectedMonoActionGroup is ScenarioMAGroup)
        {
            selectedScenarioGroup = model.selectedMonoActionGroup as ScenarioMAGroup;

            ScenarioTitleText.Value = selectedScenarioGroup.ScenarioName;
            ScenarioDescriptionText.Value = selectedScenarioGroup.ScenarioDescription;

            
            foreach (ScenarioMACore core in selectedScenarioGroup.ActionCores)
            {
                ScenarioStepViewPresenter scenarioStepViewPresenter = Instantiate(stepViewPresenterPrefab, m_parent);

                scenarioStepViewPresenter.SetScenarioStepView(core);

                scenarioStepViewPresenters.Add(scenarioStepViewPresenter);
            }
        }
        else
        {
            PLDebug.LogError(this, $"Была установлена некорректная группа для сценария!!!");
        }

    }

    private void OnScenarioEnded(MonoActionGroupEndingData obj)
    {
        IsScenarioInProgress.Value = false;
        IsScenarioInProgressReversed.Value = true;

    }

    private void CoreOnStepCompleted(MonoActionScenarioStepCompletionData data)
    {
        data.scenarioStepCore.OnComponentCompleted -= OnCoreComponentCompleted;

        scenarioStepViewPresenters[model.CurrentStepIndex - 1].SetCompletionState(data.scenarioStepCore.CompletedComponentsCount);
    }

    private void CoreOnStepSetup(MonoActionScenarioStepSetupData data)
    {
        currentCore = data.scenarioStepCore;

        componentStepIndex = 0;

        data.scenarioStepCore.OnComponentCompleted += OnCoreComponentCompleted;
    }

    private void OnCoreComponentCompleted(MonoActionComponentCompletionData data)
    {
        if (model==null)
        {
            InjectModel(scenarioController);
        }
        if (data.ignoreStatisticsOutput || (model.SelectedScenarioMode != data.completedComponent.ActivationScenarioMode && !data.completedComponent.AlwaysActivate)) return;
            scenarioStepViewPresenters[model.CurrentStepIndex].SetCompletionState(componentStepIndex);
        componentStepIndex++;

    }
}*/