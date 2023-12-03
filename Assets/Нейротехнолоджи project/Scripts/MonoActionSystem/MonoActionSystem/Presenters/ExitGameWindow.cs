using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameWindow : MonoBehaviour
{
    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;

    public Action onExit;

    private void Start()
    {
        noButton.onClick.AddListener(CloseWindow);
        yesButton.onClick.AddListener(OnExitInvoke);
    }

    private void OnDestroy()
    {
        noButton.onClick.RemoveListener(CloseWindow);
        yesButton.onClick.RemoveListener(OnExitInvoke);
    }

    private void OnExitInvoke()
    {
        onExit?.Invoke();
    }

    private void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}
