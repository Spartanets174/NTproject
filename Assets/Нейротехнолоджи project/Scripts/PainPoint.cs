using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PainPoint : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private BodyParts m_bodyPart;
    public BodyParts BodyPart=> m_bodyPart;

    public event Action OnClick;


    private bool isChosen;
    public bool IsChosen=>isChosen;



    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isChosen = !isChosen;
        OnClick.Invoke();
        image.DOFade(0.5f, 0);
    }

    public void ResetPoint()
    {
        isChosen = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isChosen)
        {
            image.DOFade(0.7f, 0);
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!isChosen)
        {
            SetNormalState();
        }       
    }

    public void SetNormalState()
    {
        image.DOFade(1, 0);
    }
}
