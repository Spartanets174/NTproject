using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MonoActionController;

public class ScenarioInvokerView : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button StartExamButton;
    [SerializeField]
    private Button StartTrainingButton;
    [SerializeField]
    private Button SelectManButton;
    [SerializeField]
    private Button SelectWomanButton;

    [Space, Header("Objects")]
    [SerializeField]
    private GameObject ScenarioModeButtons;
    [SerializeField]
    private GameObject GenderModeButtons;

    private ScenarioController scenarioController;

    private GenderMode genderMode;
    private ScenarioMode scenarioMode;

    public void Start()
    {
        scenarioController =FindObjectOfType<ScenarioController>();

        StartExamButton.onClick.AddListener(StartExam);
        StartTrainingButton.onClick.AddListener(StartTraining);
        SelectManButton.onClick.AddListener(SelectMan);
        SelectWomanButton.onClick.AddListener(SelectWoman);

        ScenarioModeButtons.SetActive(false);
    }

    private void OnDestroy()
    {
        StartExamButton.onClick.RemoveListener(StartExam);
        StartTrainingButton.onClick.RemoveListener(StartTraining);
        SelectManButton.onClick.RemoveListener(SelectMan);
        SelectWomanButton.onClick.RemoveListener(SelectWoman);
    }

    private void SelectMan()
    {
        genderMode= GenderMode.Man;
        ToggleWindows();
    }
    private void SelectWoman()
    {
        genderMode = GenderMode.Woman;
        ToggleWindows();
    }

    private void StartExam()
    {
        scenarioMode = ScenarioMode.Exam;
        scenarioController.StartScenario(scenarioMode, genderMode);
        gameObject.SetActive(false);
    }

    private void StartTraining()
    {
        scenarioMode = ScenarioMode.Training;
        scenarioController.StartScenario(scenarioMode, genderMode);
        gameObject.SetActive(false);
    }

    private void ToggleWindows()
    {
        GenderModeButtons.active = !GenderModeButtons.active;
        ScenarioModeButtons.active = !ScenarioModeButtons.active;
    }
}
