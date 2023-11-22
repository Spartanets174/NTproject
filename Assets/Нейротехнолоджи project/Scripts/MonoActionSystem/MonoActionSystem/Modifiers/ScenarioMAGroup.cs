using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioMAGroup : MonoActionGroup
{
    [SerializeField]
    [TextArea(3, 5)]
    private string m_scenarioName;
    public string ScenarioName => m_scenarioName;


    [SerializeField]
    [TextArea(10, 100)]
    private string m_scenarioDescription;
    public string ScenarioDescription => m_scenarioDescription;

    [SerializeField]
    private Sprite insideFonSprite;
    [SerializeField]
    private Sprite outsideFonSprite;
    [SerializeField]
    private SpriteRenderer FonImage;

    public override void SetupNextCore()
    {
        base.SetupNextCore();

        FonImage.sprite = _currentCoreInAction.SportMode == SportMode.Inside ? insideFonSprite : outsideFonSprite;
    }
}
