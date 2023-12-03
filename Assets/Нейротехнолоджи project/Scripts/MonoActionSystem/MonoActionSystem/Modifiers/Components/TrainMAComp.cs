using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMAComp : MonoActionComponent
{
    [SerializeField, TextArea(10, 100)]
    private string m_trainText;
    public string TrainText => m_trainText;
}
