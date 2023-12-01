using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioControllerPresenter : MonoBehaviour, IBootstrapper
{
    [Header("Buttons")]
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Button nextStepButtonButton;


    [Space, Header("Objects")]
    [SerializeField]
    private PauseMenuWindow pauseWindow;

    [SerializeField]
    private ScenarioInvokerView scenarioInvokerView;
    [SerializeField]
    private ScenarioEndWindow scenarioEndWindow;
    [SerializeField]
    private HealingProcessWindow healingProcessWindow;
    [SerializeField]
    private ExitGameWindow exitGameWindow;

    public bool IsScenarioTrain { get; private set; }

    private ScenarioMAGroup selectedScenarioGroup;

    private ScenarioMACore currentCore;

    private ScenarioController scenarioController;

    public void Init()
    {
        scenarioController = FindAnyObjectByType<ScenarioController>();

        scenarioInvokerView.gameObject.SetActive(true);

        nextStepButtonButton.onClick.AddListener(NextComponent);
        pauseButton.onClick.AddListener(EnablePauseWindow);

        scenarioController.OnGroupStarted += OnScenarioStarted;
        scenarioController.OnGroupEnded += OnScenarioEnded;

        scenarioController.OnStepPreSetup += CoreOnPreStepSetup;

        scenarioController.OnHealingProcessStarted += SetupHealingProcessWindow;

        pauseWindow.onEndGame += EndGame;
        scenarioInvokerView.onEndGame+= EndGame;
        exitGameWindow.onExit += scenarioController.EndScenario;
    }

    private void SetupHealingProcessWindow()
    {
        healingProcessWindow.Setup((HealingMAComp)currentCore.components[currentCore.CurrentComponentIndex]);

        healingProcessWindow.OnPointsChosen += EnableNextStepButton;
        healingProcessWindow.OnPointsUnChosen += DisableNextStepButton;
    }

    private void EndGame()
    {
        exitGameWindow.gameObject.SetActive(true);        
    }

   

    private void EnablePauseWindow()
    {
        pauseWindow.gameObject.SetActive(true);
    }

    private void OnScenarioStarted()
    {
        if (scenarioController.SelectedScenarioMode == ScenarioMode.Training)
        {
            IsScenarioTrain = true;
        }

        if (scenarioController.selectedMonoActionGroup is ScenarioMAGroup)
        {
            selectedScenarioGroup = scenarioController.selectedMonoActionGroup as ScenarioMAGroup;
            selectedScenarioGroup.OnGroupEnded += OnGroupEnded;
        }
        else
        {
            Debug.LogError($"Была установлена некорректная группа для сценария!!!");
        }

    }

    private void OnGroupEnded()
    {
        scenarioEndWindow.gameObject.SetActive(true);
    }

    private void OnScenarioEnded()
    {
        IsScenarioTrain = false;
    }

    private void CoreOnPreStepSetup()
    {
        currentCore = (ScenarioMACore)scenarioController.selectedMonoActionGroup.CurrentCoreInAction;
        foreach (var item in currentCore.components)
        {
            item.OnSetup += DisableNextStepButton;
            if (item is AnimationMAComp)
            {
                AnimationMAComp comp = (AnimationMAComp)item;
                comp.OnAnimationEnded += EnableNextStepButton;
            }
            if (item is RightWrongAnimationMAComp)
            {
                RightWrongAnimationMAComp comp = (RightWrongAnimationMAComp)item;
                comp.OnAnimationEnded += EnableNextStepButton;
            }
        }
    }

    private void DisableNextStepButton()
    {
        nextStepButtonButton.interactable = false;
    }

    private void EnableNextStepButton()
    {
        nextStepButtonButton.interactable = true;
    }

    private void NextComponent()
    {
        currentCore.components[currentCore.CurrentComponentIndex].CompleteComponent();
    }
}