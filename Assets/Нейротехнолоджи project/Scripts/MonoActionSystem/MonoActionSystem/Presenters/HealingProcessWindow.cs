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
    [SerializeField]
    private InstrumentController instrumentController;

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
            item.OnClick += SetLastClickedInstrument;
        }
        foreach (var instrument in instrumentController.Instruments)
        {
            instrument.OnClick += AllowPointClick;
        }
    }

    private void AllowPointClick(ClickableSpriteRenderer renderer)
    {
        foreach (var item in PainPoints)
        {
            item.IsAllowedToInteract = true;
        }
    }

    private void SetLastClickedInstrument(ClickableSpriteRenderer clickableSprite)
    {
        PainPoint painPoint = (PainPoint)clickableSprite;
        Instrument instrument = instrumentController.CurrentInstrumentHolder.Instrument == Instrument.Sacrus ? Instrument.Sacrus : Instrument.Cordus;
        painPoint.LastClickedInstrument = instrument;
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
            ScenarioController.isRightExercise = true;
        }
        else
        {
            ScenarioController.isRightExercise = false;
        }
        Debug.Log(rightCount);
    }
    private bool IsRightPoint(PainPoint point)
    {
        if (point != null)
        {
            if (point.LastClickedInstrument == point.Instrument)
            {
                ScenarioController.AddScores();
                ScenarioController.AddCombo();
                ScenarioController.AddRightDots();
                return true;
            }                  
        }
        ScenarioController.ResetCombo();
        return false;
    }

    public void ResetHealingProcess()
    {
        foreach (var item in PainPoints)
        {
            item.ResetPoint();
            item.SetNormalState();
        }
        foreach (var item in instrumentController.Instruments)
        {
            item.ResetInstrumentHolder();
            item.SetNormalState();
        }
    }

    private void SetPointsState(ClickableSpriteRenderer clickableSprite)
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

}
