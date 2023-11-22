using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioMACore : MonoActionCore
{

    [TextArea(3, 5)]
    [SerializeField]
    private string m_title;
    public string Title => m_title;


    [TextArea(10, 100)]
    [SerializeField]
    private string m_description;
    public string Description => m_description;

}
