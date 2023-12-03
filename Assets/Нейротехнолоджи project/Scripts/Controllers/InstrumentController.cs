using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstrumentController : MonoBehaviour
{
    private List<InstrumentHolder> m_instruments;
    public List<InstrumentHolder> Instruments => m_instruments;

    private InstrumentHolder m_currentInstrumentHolder;
    public InstrumentHolder CurrentInstrumentHolder=> m_currentInstrumentHolder;

    private void Awake()
    {
        m_instruments = GetComponentsInChildren<InstrumentHolder>().ToList();

        foreach (var instrument in Instruments)
        {
            instrument.IsAllowedToInteract = true;
            instrument.OnClick += OnInstrumentClick;
        }
    }

    private void OnInstrumentClick(ClickableSpriteRenderer clickableSprite)
    {
        if (m_currentInstrumentHolder != null)
        {
            m_currentInstrumentHolder.IsChosen = false;
        }
        clickableSprite.IsChosen = true;
        m_currentInstrumentHolder = (InstrumentHolder)clickableSprite;
    }
}
