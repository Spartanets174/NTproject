/*using PLCore.PLUnity.MVPReactive;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ScenarioStepViewPresenter : PresenterBehaviour<Transform>
{
    [SerializeField]
    private ToggleBlockUI m_toggleBlockActionPrefab;

    [SerializeField]
    private Transform m_parent;


    [SerializeField]
    private ImageFillHandler imageFillHandler;


    [MVPReactiveProperty]
    public ReactiveProperty<string> StepTitle = new ReactiveProperty<string>();

    [MVPReactiveProperty]
    public ReactiveProperty<string> StepDescription = new ReactiveProperty<string>();


    private ScenarioMACore currentCore;


    private List<ToggleBlockUI> toggleBlocks = new List<ToggleBlockUI>();
    private ScenarioController scenarioController;



    public void SetScenarioStepView(ScenarioMACore core)
    {
        currentCore = core;

        StepTitle.Value = core.Title;
        StepDescription.Value = core.Description;
        scenarioController = SimpleObjectFinder.TryFindComponentAtScene<ScenarioController>();

        foreach (MonoActionComponent component in core.components)
        {
            if (component.IgnoreStatisticsOutput|| (scenarioController.SelectedScenarioMode!=component.ActivationScenarioMode&&!component.AlwaysActivate)) continue;

            ToggleBlockUI toggleBlockUI = Instantiate(m_toggleBlockActionPrefab, m_parent);
            
            toggleBlockUI.SetTitleText(component.ToString());

            toggleBlocks.Add(toggleBlockUI);
        }
    }


    public void SetCompletionState(int subStepIndex)
    {
        if (subStepIndex >= toggleBlocks.Count)
            subStepIndex = toggleBlocks.Count - 1;

        for (int i = 0; i <= subStepIndex; i++)
        {
            if (!toggleBlocks[i].currentValue)
            {
                toggleBlocks[i].Toggle(true);
            }
        }
    }
}
*/