
using System;
using UnityEngine;


public class PainPoint : ClickableSpriteRenderer
{
    [SerializeField]
    private BodyParts m_bodyPart;
    public BodyParts BodyPart=> m_bodyPart;

    [SerializeField]
    private Instrument m_instrument;
    public Instrument Instrument => m_instrument;


    private Instrument m_lastClickedinstrument;
    public Instrument LastClickedInstrument
    {
        get => m_lastClickedinstrument;
        set => m_lastClickedinstrument = value;
    }

    public void ResetPoint()
    {
        isChosen = false;
        m_isAllowedToInteract = false;
    }
}
