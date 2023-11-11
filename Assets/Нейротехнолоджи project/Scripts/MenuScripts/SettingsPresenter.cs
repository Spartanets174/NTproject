using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPresenter : MonoBehaviour, IBootstrapper
{
    [Space, Header("Action UI")]
    [SerializeField]
    private Button closeButton;
    [SerializeField]
    private Slider soundSlider;
    [SerializeField]
    private Slider musicSlider;


    [Space, Header("UI elements")]   
    [SerializeField]
    private GameObject settingsWindow;
    [SerializeField]
    private Image soundImage;
    [SerializeField]
    private Image musicImage;

    [Space, Header("Images")]
    [SerializeField]
    private Sprite enabledSoundSprite;
    [SerializeField]
    private Sprite enabledMusicSprite;
    [SerializeField]
    private Sprite disabledSoundSprite;
    [SerializeField]
    private Sprite disabledMusicSprite;


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

        closeButton.onClick.AddListener(TurnOffSettingsWindow);
        soundSlider.onValueChanged.AddListener(OnSoundValueChanged);
        musicSlider.onValueChanged.AddListener(OnMusicValueChanged);
        soundSlider.value = settingsController.GetSoundVolume();
        musicSlider.value = settingsController.GetMusicVolume();
    }

    private void OnSoundValueChanged(float value)
    {
        soundImage.sprite = value == 0 ? disabledSoundSprite : enabledSoundSprite;
        settingsController.ChangeSoundLevel(value);
    }

    private void OnMusicValueChanged(float value)
    {
        musicImage.sprite = value == 0 ? disabledMusicSprite : enabledMusicSprite;
        settingsController.ChangeMusicLevel(value);
    }


    private void TurnOffSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }
}
