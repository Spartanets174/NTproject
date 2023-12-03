using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootstrapController : MonoBehaviour
{
    [SerializeField]
    private BootstrapMode bootstrapMode;

    [SerializeField]
    private List<GameObject> bootstrappers;

    private void Start()
    {
        if (bootstrapMode == BootstrapMode.Start)
        {
            InitBootsrtapperrs();
        }
    }

    private void Awake()
    {
        if (bootstrapMode == BootstrapMode.Awake)
        {
            InitBootsrtapperrs();
        }
    }

    private void InitBootsrtapperrs()
    {
        foreach (var bootstrapper in bootstrappers)
        {
            if (bootstrapper.TryGetComponent(out IBootstrapper IBootstrapper))
            {
                IBootstrapper.Init();
            }
            else
            {
                Debug.LogWarning($"Не найден компонен IBootstrapper в {bootstrapper.name}");
            }
        }
    }
}

public enum BootstrapMode
{
    Start,
    Awake
}