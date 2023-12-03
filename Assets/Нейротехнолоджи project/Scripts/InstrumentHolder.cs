using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentHolder : ClickableSpriteRenderer
{
    [SerializeField]
    private Instrument m_instrument;
    public Instrument Instrument => m_instrument;

    public void ResetInstrumentHolder()
    {
        isChosen = false;
    }
}
