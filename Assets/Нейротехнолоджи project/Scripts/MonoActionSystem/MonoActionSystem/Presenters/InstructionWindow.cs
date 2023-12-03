using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstructionWindow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI instructionText;
    [SerializeField]
    private Button closeButton;


    public event Action OnWindowClosed;
    private void Start()
    {
        closeButton.onClick.AddListener(CloseWindow);
    }

    private void CloseWindow()
    {
        gameObject.SetActive(false);
        OnWindowClosed?.Invoke();
    }

    public void OpenWindow()
    {
        Debug.Log("SF");
        gameObject.SetActive(true);
    }

    public void SetData(string text)
    {
        instructionText.text = text;
    }
}
