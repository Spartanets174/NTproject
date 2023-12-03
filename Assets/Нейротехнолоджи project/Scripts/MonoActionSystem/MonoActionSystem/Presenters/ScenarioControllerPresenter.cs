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
    private ExitGameWindow exitGameWindow;
    [SerializeField]
    private InstructionWindow instructionWindow;

    public bool IsScenarioTrain { get; private set; }

    private ScenarioMAGroup selectedScenarioGroup;

    

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
        scenarioController.HealingProcessWindow.OnPointsChosen += EnableNextStepButton;
        scenarioController.HealingProcessWindow.OnPointsUnChosen += DisableNextStepButton;

        instructionWindow.OnWindowClosed += NextComponent;
        pauseWindow.onEndGame += EndGame;
        scenarioInvokerView.onEndGame+= EndGame;
        exitGameWindow.onExit += scenarioController.EndScenario;
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
        foreach (var item in scenarioController.CurrentCore.components)
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
            if (item is TrainMAComp)
            {
                item.OnSetup += SetTrainData;
                
            }
        }
    }

    private void SetTrainData(MonoActionComponent component)
    {
        TrainMAComp comp = (TrainMAComp)component;
        instructionWindow.SetData(comp.TrainText);
        instructionWindow.OpenWindow();
    }

    private void DisableNextStepButton(MonoActionComponent component)
    {
        nextStepButtonButton.interactable = false;
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
        scenarioController.CurrentCore.components[scenarioController.CurrentCore.CurrentComponentIndex].CompleteComponent();
    }
}