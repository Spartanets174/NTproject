using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour, IBootstrapper
{
    [SerializeField]
    private DbManager DB;
    [SerializeField]
    private PlayerData playerData;
    public void Init()
    {
        playerData = DB.PlayerData;
        playerData.playerId = SaveSystem.LoadPlayer();
        playerData = DB.PlayerData;
        if (playerData.playerId != -1)
        {
            GetPlayerData();
        }
    }
    public void IsNicknameInBase(string Nick)
    {
        bool hasName = false;
        List<UserData> nickList = DB.SelectUsers();
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
        int id = DB.InsertToPlayers(Nick);        
        playerData.playerId = id;
        playerData.playerName = Nick;
        playerData.playerScores = 0;

        SaveSystem.SavePlayer(id);

        SceneManager.LoadScene("menu");
    }

    private void GetPlayerData()
    {
        playerData.playerId = SaveSystem.LoadPlayer();
        UserData userData;
        if (playerData.playerId!=-1)
        {
            userData = DB.SelectUser(playerData.playerId);
        }
        else
        {
            userData = DB.SelectUserByNick(playerData.playerName);
            playerData.playerId = userData.UserId;
        }
        
        playerData.playerName = userData.UserName;
        playerData.playerScores = userData.UserScore;

        SceneManager.LoadScene("menu");
    }
}