/*using PLCore.PLUnity.EntitySystem;
using PLCore.PLUnity.MVPReactive;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class InspectableControllerPresenter : PresenterBehaviour<InspectableController>, IBootstrapper
{
    [MVPReactiveProperty]
    public ReactiveProperty<bool> IsInspectingModeEnabled = new ReactiveProperty<bool>();

    [MVPReactiveProperty]
    public ReactiveProperty<InspectableDataArgs> InspectableData = new ReactiveProperty<InspectableDataArgs>();

    private bool m_isInitialized = false;
    public bool IsInitialized => m_isInitialized;

    public void InitializeBootstrapper()
    {
        m_isInitialized = true;
        OnInjectModel(SimpleObjectFinder.TryFindComponentAtScene<InspectableController>());
    }


    protected override void OnInjectModel(InspectableController model)
    {
        base.OnInjectModel(model);

        // ...
    }

    protected override void OnDestroy()
    {
        // ...

        base.OnDestroy();
    }
}*/