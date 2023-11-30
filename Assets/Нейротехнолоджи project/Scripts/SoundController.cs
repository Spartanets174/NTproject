using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour,IBootstrapper
{
    private List<Button> soundButtons;
    SettingsController settingsController;
    public void Init()
    {
        soundButtons = FindObjectsOfType<Button>(true).ToList();
        settingsController = FindObjectOfType<SettingsController>();

        foreach (var button in soundButtons)
        {
            button.onClick.AddListener(settingsController.PlayClickSound);
        }
    }
}
