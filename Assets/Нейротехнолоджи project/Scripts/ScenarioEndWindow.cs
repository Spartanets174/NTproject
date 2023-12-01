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
    [SerializeField]
    private TextMeshProUGUI comboText;


    private ScenarioController scenarioController;  

    public void Start()
    {
        scenarioController = FindObjectOfType<ScenarioController>();
        endGameButton.onClick.AddListener(EndGame);
        dotsText.text = $"{scenarioController.RightDotsCount}/{scenarioController.DotsCount}";
        scoresText.text = $"{scenarioController.Score}";
        comboText.text = $"{scenarioController.Combo}";
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
