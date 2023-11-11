using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuPresenter : MonoBehaviour, IBootstrapper
{
    [Header("Buttons")]
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button recordsTableButton;
    [SerializeField]
    private Button exitButton;

    [Space,Header("UI elements")]
    [SerializeField]
    private GameObject settingsWindow;
    [SerializeField]
    private GameObject recordsTable;

    MenuController menuController;

    
    public void Init()
    {
        menuController = FindObjectOfType<MenuController>();
        

        exitButton.onClick.AddListener(menuController.Exit);
        playButton.onClick.AddListener(LoadPlayScene);
        settingsButton.onClick.AddListener(TurnOnSettingsWindow);
        recordsTableButton.onClick.AddListener(TurnOnRecordsTable);
    }
    
    private void OnDestroy()
    {        
        playButton.onClick.RemoveListener(LoadPlayScene);
        settingsButton.onClick.RemoveListener(TurnOnSettingsWindow);
        recordsTableButton.onClick.RemoveListener(TurnOnRecordsTable);
    }

    private void LoadPlayScene()
    {
        menuController.LoadPlayScene();
    }
    private void TurnOnSettingsWindow()
    {
        settingsWindow.SetActive(true);
    }
    private void TurnOnRecordsTable()
    {
        recordsTable.SetActive(true);
    }
}
