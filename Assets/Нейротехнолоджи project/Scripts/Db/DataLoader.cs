using Renci.SshNet.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour, IBootstrapper
{
    [SerializeField]
    private DbManager DB;
    [SerializeField]
    private WebController webController;
    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private DataMode dataMode;

    private DataController currentDataController;
    public void Init()
    {
        playerData.playerId = SaveSystem.LoadPlayer();
        switch (dataMode)
        {
            case DataMode.Web:
                currentDataController = webController;
                break;
            case DataMode.Standalone:
                currentDataController = DB;
                break;
        }
        currentDataController.Init();
        currentDataController.AddComponent<DontDestroyOnLoad>();
        if (playerData.playerId != -1)
        {
            GetPlayerData();
        }
    }
    public void IsNicknameInBase(string Nick)
    {
        bool hasName = false;
        List<UserData> nickList = new();
        currentDataController.SelectUsers((List<UserData> userDatas) =>
        {
            nickList = userDatas;
        });
        for (int i = 0; i < nickList.Count; i++)
        {
            if (nickList[i].UserName == Nick)
            {
                playerData.playerName = Nick;
                hasName = true;
            }
        }
        if (nickList.Count == 0 || !hasName)
        {
            CreateNewPlayer(Nick);
        }
        else
        {
            GetPlayerData();
        }
    }

    private void CreateNewPlayer(string Nick)
    {
        currentDataController.InsertToPlayers(Nick, (int id) =>
        {
            playerData.playerId = id;
            playerData.playerName = Nick;
            playerData.playerScores = 0;
            playerData.playerCombo = 0;

            SaveSystem.SavePlayer(id);

            SceneManager.LoadScene("menu");
        });
    }

    private void GetPlayerData()
    {
        playerData.playerId = SaveSystem.LoadPlayer();
        if (playerData.playerId!=-1)
        {
            currentDataController.SelectUser(playerData.playerId, SetPlayerData);
        }
        else
        {
            currentDataController.SelectUserByNick(playerData.playerName, SetPlayerData);                  
        }                      
    }

    private void SetPlayerData(UserData userData)
    {
        playerData.playerId = userData.UserId;
        playerData.playerName = userData.UserName;
        playerData.playerScores = userData.UserScore;
        playerData.playerCombo = userData.UserCombo;
        SaveSystem.SavePlayer(playerData.playerId);
        SceneManager.LoadScene("menu");
    }
}

public enum DataMode
{
    Web,
    Standalone
}