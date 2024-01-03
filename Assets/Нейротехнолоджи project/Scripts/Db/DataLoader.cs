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
    private string currentNick;
    public void Init()
    {       
        switch (dataMode)
        {
            case DataMode.Web:
                currentDataController = webController;
                break;
            case DataMode.Standalone:
                currentDataController = DB;
                break;
        }
        playerData.ClearData();
        currentDataController.Init();
        currentDataController.AddComponent<DontDestroyOnLoad>();

        /*        playerData.playerId = SaveSystem.LoadPlayer();
                if (playerData.playerId != -1)
                {
                    GetPlayerData();
                }
        */
    }
    public void IsNicknameInBase(string Nick)
    {
        currentNick = Nick;
        currentDataController.SelectUsers(CheckNickname);     
    }

    private void CheckNickname(List<UserData> userDatas)
    {
        bool hasName = false;
        for (int i = 0; i < userDatas.Count; i++)
        {
            if (userDatas[i].UserName == currentNick)
            {
                playerData.playerName = currentNick;
                hasName = true;
            }
        }
        if (userDatas.Count == 0 || !hasName)
        {
            CreateNewPlayer(currentNick);
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

/*            SaveSystem.SavePlayer(id);
*/
            SceneManager.LoadScene("menu");
        });
    }

    private void GetPlayerData()
    {
/*        playerData.playerId = SaveSystem.LoadPlayer();
*/        if (playerData.playerId>0)
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
/*        SaveSystem.SavePlayer(playerData.playerId);
*/        SceneManager.LoadScene("menu");
    }
}

public enum DataMode
{
    Web,
    Standalone
}