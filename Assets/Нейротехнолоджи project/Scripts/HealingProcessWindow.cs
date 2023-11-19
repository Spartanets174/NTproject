using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealingProcessWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject man;
    [SerializeField]
    private GameObject woman;

    private List<PainPoint> PainPoints;
    private HealingMAComp HealingMAComp;
    private ScenarioController ScenarioController;

    public event Action OnPointsChosen;
    public event Action OnPointsUnChosen;
    public void Setup(HealingMAComp healingMAComp)
    {
        HealingMAComp = healingMAComp;
        ScenarioController = FindFirstObjectByType<ScenarioController>();
       
        if (ScenarioController.SelectedGenderMode == GenderMode.Man)
        {
            SetupForMan();
        }
        else
        {
            SetupForWoman();
        }

        HealingMAComp.OnComplete += OnComplete;
        gameObject.SetActive(true);
        ResetHealingProcess();

        foreach (var item in HealingMAComp.BodyParts)
        {
            ScenarioController.AddDots();
        }
        foreach (var item in PainPoints)
        {
            item.OnClick += SetPointsState;
        }
    }

    private void SetupForMan()
    {
        man.SetActive(true);
        woman.SetActive(false);
        PainPoints = man.GetComponentsInChildren<PainPoint>().ToList();
    }
    private void SetupForWoman()
    {
        man.SetActive(false);
        woman.SetActive(true);
        PainPoints = woman.GetComponentsInChildren<PainPoint>().ToList();
    }

    private void OnComplete()
    {
        int count=0;
        foreach (var part in HealingMAComp.BodyParts)
        {
            foreach (var point in PainPoints)
            {
                if (point.IsChosen&& part == point.BodyPart)
                {
                    count++;
                    ScenarioController.AddScores();
                    ScenarioController.AddCombo();
                    ScenarioController.AddRightDots();
                }
                else
                {
                    ScenarioController.ResetCombo();
                }
            }
        }
        if (count== HealingMAComp.BodyParts.Count)
        {
            HealingMAComp.IsRight = true;
        }
        else
        {
            HealingMAComp.IsRight = false;
        }
        this.gameObject.SetActive(false);
    }
    public void ResetHealingProcess()
    {
        foreach (var item in PainPoints)
        {
            item.ResetPoint();
        }
    }

    private void SetPointsState()
    {        
        if (PainPoints.FindAll(x=>x.IsChosen).Count>=2)
        {
            TogglePointsState(false);
            OnPointsChosen?.Invoke();
        }
        else
        {
            TogglePointsState(true);
            OnPointsUnChosen?.Invoke();
        }
    }

    private void TogglePointsState(bool state)
    {
        foreach (var item in PainPoints)
        {
            item.IsAllowedToClick = state;
        }
    }
}
