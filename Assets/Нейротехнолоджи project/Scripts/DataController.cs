using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataController : MonoBehaviour, IBootstrapper
{
    [SerializeField] 
    protected PlayerData playerData;
    public PlayerData PlayerData => playerData;

    protected static string connectionString;
    public abstract void Init();
    public abstract void InsertToPlayers(string Name, Action<int> callback);

    public abstract void UpdatePlayerScore(Action callback);

    public abstract void SelectUsers(Action<List<UserData>> callback);

    public abstract void SelectUser(int id, Action<UserData> callback);

    public abstract void SelectUserByNick(string Name, Action<UserData> callback);
}
