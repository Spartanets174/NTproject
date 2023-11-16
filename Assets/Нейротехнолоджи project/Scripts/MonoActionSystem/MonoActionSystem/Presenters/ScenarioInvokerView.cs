/*using PLCore.PLInteractionToolkit;
using PLCore.PLUnity.MVPReactive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioInvokerView : PresenterBehaviour<ScenarioController>, IBootstrapper
{
    private bool m_isInitialized;
    public bool IsInitialized => m_isInitialized;

    private TabMenuPresenter tabMenuPresenter;

    public void InitializeBootstrapper()
    {
        if (m_isInitialized) return;

        m_isInitialized = true;

        InjectModel(SimpleObjectFinder.TryFindComponentAtScene<ScenarioController>());

        tabMenuPresenter = SimpleObjectFinder.TryFindComponentAtScene<TabMenuPresenter>();
    }

    protected override void OnInjectModel(ScenarioController model) { }

    protected override void OnRemoveModel(ScenarioController model) { }

    private void OnEnable()
    {
        this.gameObject.SetActive(true);

        if (tabMenuPresenter != null)
            tabMenuPresenter.isAllowedToOpenTabMenu = false;
    }

    [MVPMethod]
    public void StartExam()
    {
        model.StartScenario(MonoActionController.ScenarioMode.Exam);

        this.gameObject.SetActive(false);
        
        if (tabMenuPresenter != null)
            tabMenuPresenter.isAllowedToOpenTabMenu = true;
    }

    [MVPMethod]
    public void StartTraining()
    {
        model.StartScenario(MonoActionController.ScenarioMode.Training);

        this.gameObject.SetActive(false);

        if (tabMenuPresenter != null)   
            tabMenuPresenter.isAllowedToOpenTabMenu = true;
    }
}
*/