
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///     Будет обрабатывать текущий шаг и текущее действие
///     Если мы проходим сценарий - отображаем шаги и текущий шаг
///     Если мы сталкиваемся с каким-то специфичным шагом - мы отображаем инфу о нём
/// </summary>
public class ScenarioController : MonoActionController, IBootstrapper
{
    public event Action OnStepSetup = null;
    public event Action OnStepPreSetup = null;
    public event Action OnStepCompleted = null;
    public event Action OnScenarioStarted = null;
    public event Action OnScenarioEnded = null;
    public event Action OnHealingProcessStarted = null;

    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private HealingProcessWindow healingProcessWindow;
    public HealingProcessWindow HealingProcessWindow => healingProcessWindow;

    [Header("Characters")]
    [SerializeField]
    private List<Character> m_womanPrefabs;
    public List<Character> WomanPrefabs => m_womanPrefabs;

    [SerializeField]
    private List<Character> manPrefabs;

    public List<Character> ManPrefabs => manPrefabs;

    private bool m_isHealingProcess;
    public bool IsHealingProcess
    {
        get { return m_isHealingProcess;}
        set 
        {  
            m_isHealingProcess = value;
            if (m_isHealingProcess)
            {
                OnHealingProcessStarted?.Invoke();
            }
        }
    }

    private ScenarioMACore currentCore;
    public ScenarioMACore CurrentCore=> currentCore;

    public bool isRightExercise { get; set; } = false;

    private int currentStepIndex;
    public int CurrentStepIndex => currentStepIndex;

    private int m_scores;
    public int Score => m_scores;

    private int maxCombo;
    private int m_combo;
    public int Combo
    {
        get
        {
            return Mathf.Max(m_combo, maxCombo);
        }
    }

    private int m_rightDotsCount;
    public int RightDotsCount => m_rightDotsCount;

    private int m_dotsCount;
    public int DotsCount => m_dotsCount;

    private DataController dataController;

    public DataController DataController => dataController;

    


    public void Init()
    {
        dataController = FindObjectOfType<DataController>();       
    }

    private void OnDestroy()
    {
        if (selectedMonoActionGroup != null)
        {
            selectedMonoActionGroup.OnCorePreSetup -= StepPreStartedHandler;
            selectedMonoActionGroup.OnCoreSetup -= StepStartedHandler;
            selectedMonoActionGroup.OnCoreCompleted -= StepCompletedHandler;
        }
    }


    [ContextMenu("Начать сценарий")]
    public void StartScenario(ScenarioMode scenarioMode, GenderMode genderMode)
    {

        SelectGroup(0);

        selectedScenarioMode = scenarioMode;
        selectedGenderMode = genderMode;

        selectedMonoActionGroup.OnCorePreSetup += SetCurrentCore;
        selectedMonoActionGroup.OnCorePreSetup += StepPreStartedHandler;
        
        selectedMonoActionGroup.OnCoreSetup += StepStartedHandler;
        selectedMonoActionGroup.OnCoreCompleted += StepCompletedHandler;
        OnHealingProcessStarted += SetupHealingProcessWindow;

        StartSelectedGroup();
        OnScenarioStarted?.Invoke();
    }

    [ContextMenu("Закончить сценарий")]
    public void EndScenario()
    {
        if (_selectedMonoActionGroup != null)
        {
            selectedMonoActionGroup.OnCorePreSetup -= StepPreStartedHandler;
            selectedMonoActionGroup.OnCorePreSetup -= SetCurrentCore;
            selectedMonoActionGroup.OnCoreSetup -= StepStartedHandler;
            selectedMonoActionGroup.OnCoreCompleted -= StepCompletedHandler;
            OnHealingProcessStarted -= SetupHealingProcessWindow;

            currentStepIndex = 0;

            _selectedMonoActionGroup = null;
        }

        SceneManager.LoadScene("Menu");
        OnScenarioEnded?.Invoke();
    }
    private void SetCurrentCore()
    {
        currentCore = (ScenarioMACore)selectedMonoActionGroup.CurrentCoreInAction;
    }
    private void SetupHealingProcessWindow()
    {
        healingProcessWindow.Setup((HealingMAComp)currentCore.components[currentCore.CurrentComponentIndex]);       
    }
    protected override void OnGroupEndedHandler()
    {

        base.OnGroupEndedHandler();
        if (selectedScenarioMode == ScenarioMode.Exam)
        {
            if (Score> playerData.playerScores)
            {
                playerData.playerScores = Score;
                playerData.playerCombo = Combo;
                dataController.UpdatePlayerScore(null);
            }        
        }
    }


    private void StepStartedHandler()
    {
        OnStepSetup?.Invoke();
    }

    private void StepPreStartedHandler()
    {
        OnStepPreSetup?.Invoke();
    }

    private void StepCompletedHandler()
    {
        currentStepIndex++;
        OnStepCompleted?.Invoke();
    }

    public void AddScores()
    {
        m_scores += 10 + 10 * m_combo;
    }

    public void AddCombo()
    {
        m_combo++;
    }
    public void ResetCombo()
    {
        if (m_combo > maxCombo)
        {
            maxCombo = m_combo;
        }
        m_combo=0;
    }
    public void MinusRightDots()
    {
        m_rightDotsCount --;
    }
    public void AddDots()
    {
        m_dotsCount++;
    }
    public void AddRightDots()
    {
        m_rightDotsCount++;
    }
}
