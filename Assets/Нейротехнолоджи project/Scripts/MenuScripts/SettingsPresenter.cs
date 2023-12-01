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

    [Space, Header("Sound images")]
    [SerializeField]
    private Sprite enabledSoundSprite;
    [SerializeField]
    private Sprite halfSoundSprite;
    [SerializeField]
    private Sprite semihalfSoundSprite;
    [SerializeField]
    private Sprite disabledSoundSprite;

    [Space, Header("Music images")]
    [SerializeField]
    private Sprite enabledMusicSprite;
    [SerializeField]
    private Sprite halfMusicSprite;
    [SerializeField]
    private Sprite semihalfMusicSprite;
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
        if (value <= 1 && value > 0.66f)
        {
            soundImage.sprite = enabledSoundSprite;
        }
        if (value <= 0.66f && value > 0.33f)
        {
            soundImage.sprite = halfSoundSprite;
        }
        if (value <= 0.33f && value >0)
        {
            soundImage.sprite = semihalfSoundSprite;
        }
        if (value == 0)
        {
            soundImage.sprite = disabledSoundSprite;
        }
        
        settingsController.ChangeSoundLevel(value);
    }

    private void OnMusicValueChanged(float value)
    {
        if (value <= 1 && value > 0.66f)
        {
            musicImage.sprite = enabledMusicSprite;
        }
        if (value <= 0.66f && value > 0.33f)
        {
            musicImage.sprite = halfMusicSprite;
        }
        if (value <= 0.33f && value > 0)
        {
            musicImage.sprite = semihalfMusicSprite;
        }
        if (value == 0)
        {
            musicImage.sprite = disabledMusicSprite;
        }

        settingsController.ChangeMusicLevel(value);
    }


    private void TurnOffSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }
}
