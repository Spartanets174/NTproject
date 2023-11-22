using System;
using System.Collections.Generic;
using System.Drawing;
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

        HealingMAComp.OnCompleteEvent.AddListener(OnComplete);
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
        CheckPoints();
        ResetPoints();
        this.gameObject.SetActive(false);
    }
    private void CheckPoints()
    {
        int rightCount = 0;
        List<PainPoint> chosenPainPoints = PainPoints.FindAll(x => x.IsChosen);
        if (chosenPainPoints.Count> HealingMAComp.BodyParts.Count)
        {
            ScenarioController.ResetCombo();
            return;
        }
        foreach (var part in HealingMAComp.BodyParts)
        {
            PainPoint painPoint = chosenPainPoints.Find(x=>x.BodyPart == part);
            if (IsRightPoint(painPoint))
            {
                rightCount++;
            }
        }
        if (rightCount == HealingMAComp.BodyParts.Count)
        {
            HealingMAComp.CurrentCharacter.isRightExercise = true;
        }
        else
        {
            HealingMAComp.CurrentCharacter.isRightExercise = false;
        }
        Debug.Log(rightCount);
    }
    private bool IsRightPoint(PainPoint point)
    {
        if (point != null)
        {
            ScenarioController.AddScores();
            ScenarioController.AddCombo();
            ScenarioController.AddRightDots();
            return true;
        }
        else
        {
            ScenarioController.ResetCombo();
            return false;
        }
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
           /* TogglePointsState(false);*/
            OnPointsChosen?.Invoke();
        }
        else
        {
            /*TogglePointsState(true);*/
            OnPointsUnChosen?.Invoke();
        }
    }

    private void ResetPoints()
    {
        foreach (var item in PainPoints)
        {
            item.SetNormalState();
        }
    }
}
