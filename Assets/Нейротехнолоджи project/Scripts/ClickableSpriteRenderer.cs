using System;
using DG.Tweening;
using UnityEngine;

public class ClickableSpriteRenderer : MonoBehaviour
{

    public event Action<ClickableSpriteRenderer> OnClick;


    protected bool isChosen;
    public bool IsChosen
    {
        get { return isChosen; }
        set 
        { 
            isChosen = value;
            if (!isChosen)
            {
                SetNormalState();
            }
        }
    }
    protected bool m_isAllowedToInteract;
    public bool IsAllowedToInteract
    {
        get { return m_isAllowedToInteract; }
        set
        {
            m_isAllowedToInteract = value;
        }
    }



    private SpriteRenderer image;

    private void Start()
    {
        image = GetComponent<SpriteRenderer>();
    }
   
    private void OnMouseDown()
    {
        if (IsAllowedToInteract)
        {
            isChosen = !isChosen;
            OnClick.Invoke(this);
            image.DOFade(0.5f, 0);
        }
    }

  

    public void OnMouseEnter()
    {
        if (!isChosen&& IsAllowedToInteract)
        {
            image.DOFade(0.7f, 0);
        }
    }
    private void OnMouseDrag()
    {
        if (!isChosen&& IsAllowedToInteract)
        {
            image.DOFade(0.7f, 0);
        }
    }

    void OnMouseExit()
    {
        if (!isChosen&& IsAllowedToInteract)
        {
            SetNormalState();
        }
    }

    public void SetNormalState()
    {
        image.DOFade(1, 0);
    }
}
