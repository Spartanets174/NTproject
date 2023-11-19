using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;


/// <summary>
///     явл€етс€ €дром выполнени€ действий (нека€ группа)
///     —одержит в себе список определЄнных компонентов, которые выполн€ют свои действи€ в заданном пор€дке
///     ћожно использовать как шаг сценари€
/// </summary>
public class MonoActionCore : MonoBehaviour
{   
   
    public event Action OnCoreSetup = null;
    public event Action OnComponentSetup = null;
    public event Action OnComponentCompleted = null;
    public event Action OnAllComponentsEndedWork = null;

    [SerializeField]
    private Transform parentToSpawn;


    [Header("Mode")]
    [SerializeField] protected ScenarioMode m_activationScenarioMode;
    public ScenarioMode ActivationScenarioMode => m_activationScenarioMode;

    [SerializeField] protected bool m_alwaysActivate = true;
    public bool AlwaysActivate => m_alwaysActivate;

    protected ScenarioMode selectedScenarioMode;
    public ScenarioMode SelectedScenarioMode => selectedScenarioMode;


    protected int _completedComponents = 0;
    public int CompletedComponentsCount => _completedComponents;

    protected bool _isCoreCompleted = false;
    protected int currentCompIndex = -1;
    public int CurrentComponentIndex => currentCompIndex;
    private List<MonoActionComponent> _components;
    public List<MonoActionComponent> components
    {
        get
        {
            if (_components == null || _components.Count == 0)
            {
                GetChildComponents();
            }

            return _components;
        }
    }

    private Character currentCharacter;

    private void GetChildComponents()
    {
        _components = GetComponentsInChildren<MonoActionComponent>().ToList();
    }

    public virtual void SetupCore(ScenarioMode scenarioMode, Character character)
    {
        currentCharacter = Instantiate(character,Vector3.zero,Quaternion.identity, parentToSpawn);
        currentCharacter.transform.localPosition = Vector3.zero;

        selectedScenarioMode = scenarioMode;

        GetChildComponents();

        SetupNextComponentByOrder();

        OnCoreSetup?.Invoke();
    }

    private void SetupNextComponentByOrder()
    {
        if (currentCompIndex > 0)
        {
            _components[currentCompIndex].OnComplete -= OnSingleComponentCompleted;
        }

        currentCompIndex++;
        if (_components[currentCompIndex] is HealingMAComp)
        {           
            currentCharacter.gameObject.SetActive(false);
        }
        else
        {
            currentCharacter.gameObject.SetActive(true);
        }
        _components[currentCompIndex].OnComplete += OnSingleComponentCompleted; 
        _components[currentCompIndex].SetRuntimeMode(selectedScenarioMode);
        _components[currentCompIndex].SetupComponent(currentCharacter);
        OnComponentSetup?.Invoke();
    }

    public virtual void ResetCore()
    {

        currentCompIndex = -1;
        Destroy(currentCharacter);
        _isCoreCompleted = false;
        _completedComponents = 0;

        for (int i = 0; i < _components.Count; i++)
        {
            _components[i].OnComplete -= OnSingleComponentCompleted;
            _components[i].ResetComponent();
        }
    }


    protected virtual void OnSingleComponentCompleted()
    {
        

        _completedComponents++;

        OnComponentCompleted?.Invoke();

        if (_completedComponents >= _components.Count)
        {
            OnAllComponentsCompleted();
        }
        else
        {
            SetupNextComponentByOrder();
        }
    }

    protected virtual void OnAllComponentsCompleted()
    {
        Destroy(currentCharacter.gameObject);
        OnAllComponentsEndedWork?.Invoke();
    }
}
