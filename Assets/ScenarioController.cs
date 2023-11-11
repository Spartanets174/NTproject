using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioController : MonoBehaviour
{

    [SerializeField]
    private Scenario scenario;


    public void StartStudy()
    {
        scenario.CurrnetScenarioMode = ScenarioMode.Study;
        scenario.StartScenario();
    }
    public void StartExam()
    {
        scenario.CurrnetScenarioMode = ScenarioMode.Exam;
        scenario.StartScenario();
    }
}

public enum ScenarioMode 
{ 
    Study,
    Exam
}
