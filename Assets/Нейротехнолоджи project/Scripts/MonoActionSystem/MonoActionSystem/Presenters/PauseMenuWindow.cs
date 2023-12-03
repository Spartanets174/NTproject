using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuWindow : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button toPasueMenuButton;
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button instructionButton;
    [SerializeField]
    private Button endGameButton;
    [Space, Header("Objects")]
    [SerializeField]
    private GameObject instructionWindow;
    [SerializeField]
    private GameObject pauseMenuWindow;
    [SerializeField]
    private GameObject pauseWindow;

   public event Action onEndGame;

    private void Start()
    {
        toPasueMenuButton.onClick.AddListener(DisableInstructionWindow);
        instructionButton.onClick.AddListener(EnableInstructionWindow);
        continueButton.onClick.AddListener(DisablePauseWindow);
        endGameButton.onClick.AddListener(EndGameButtonClick);
    }  

    private void OnDestroy()
    {
        toPasueMenuButton.onClick.RemoveListener(DisableInstructionWindow);
        instructionButton.onClick.RemoveListener(EnableInstructionWindow);
        continueButton.onClick.RemoveListener(DisablePauseWindow);
        endGameButton.onClick.RemoveListener(EndGameButtonClick);
    }
    private void EndGameButtonClick()
    {
        onEndGame?.Invoke();
    }
    private void DisableInstructionWindow()
    {
        instructionWindow.SetActive(false);
        pauseMenuWindow.SetActive(true);
    }
    private void EnableInstructionWindow()
    {
        instructionWindow.SetActive(true);
        pauseMenuWindow.SetActive(false);
    }
    private void DisablePauseWindow()
    {
        pauseWindow.SetActive(false);
    }

}