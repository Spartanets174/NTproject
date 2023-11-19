using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioEndWindow : MonoBehaviour
{
    [SerializeField]
    private Button endGameButton;
    [SerializeField]
    private TextMeshProUGUI dotsText;
    [SerializeField]
    private TextMeshProUGUI scoresText;


    private ScenarioController scenarioController;  

    public void Start()
    {
        scenarioController = FindObjectOfType<ScenarioController>();
        endGameButton.onClick.AddListener(EndGame);
        dotsText.text = $"Вы устранили {scenarioController.RightDotsCount} из {scenarioController.DotsCount} точек боли правильно!";
        scoresText.text = $"Вы набрали {scenarioController.Score} очков";
    }
    private void OnDestroy()
    {
        endGameButton.onClick.RemoveListener(EndGame);
    }

    private void EndGame()
    {
        scenarioController.EndScenario();
    }
}
