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

    private bool m_isAllowedToClick;

    private Image image;
    public bool IsAllowedToClick
    {
        get => m_isAllowedToClick;
        set => m_isAllowedToClick = value;
    }
    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsAllowedToClick)
        {
            isChosen = !isChosen;
            OnClick.Invoke();
            image.DOFade(0.5f, 0);
        }     
    }

    public void ResetPoint()
    {
        isChosen = false;
        m_isAllowedToClick = true;
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
            image.DOFade(1, 0);
        }
        
    }
}
