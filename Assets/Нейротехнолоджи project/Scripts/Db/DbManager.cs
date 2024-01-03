using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class ConnectionInfo
{
    public static string ip = "127.0.0.1";
    public static string uid = "root";
    public static string pwd = "12345";
    public static string database = "dbneiro";
}

public class DbManager : DataController
{
    public static MySqlConnection con;
   
    public override void Init()
    {
        connectionString = $"server = {ConnectionInfo.ip}; uid = {ConnectionInfo.uid}; pwd = {ConnectionInfo.pwd}; Database = {ConnectionInfo.database}; SSLMode = none";
        con = new MySqlConnection(connectionString);
        try
        {
            con.Open();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    private void OnApplicationQuit()
    {
        if (playerData!=null)
        {
            UpdatePlayerScore(closeCon);
        }
        
    }
    public void closeCon()
    {
        con.Close();
        Debug.Log("closed");
    }

    #region Player
    public override void InsertToPlayers(string Name, Action<int> callback)
    {
        string query = $"insert into {ConnectionInfo.database}.users (username,userScore, userCombo) values ('{Name}',{0},{0})";

        var command = new MySqlCommand(query, con);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        command.Dispose();
        callback?.Invoke((int)command.LastInsertedId);
    }

    public override void UpdatePlayerScore(Action callback)
    {
        string query = $"UPDATE {ConnectionInfo.database}.users SET userScore = {playerData.playerScores}, userCombo = {playerData.playerCombo} where username='{playerData.playerName}'";

        var command = new MySqlCommand(query, con);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        command.Dispose();
        callback?.Invoke();
    }
    public override void SelectUsers(Action<List<UserData>> callback)
    {
        string query = $"select * from {ConnectionInfo.database}.users";
        List<UserData> users = new ();
        MySqlCommand command = new MySqlCommand(query, con);
        try
        {
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32("idUser");
                    string name = reader.GetString("username");
                    int scores = reader.GetInt32("userScore");
                    int combo = reader.GetInt32("userCombo");
                    users.Add(new UserData(id, name, scores, combo));
                }
                /* reader.Read();*/
                /*Debug.Log(reader.GetString("name"));*/
                command.Dispose();
            }
            else
            {
                command.Dispose();
            }
        }
        catch (System.Exception ex)
        {
            command.Dispose();
            Debug.LogError(ex.Message);
        }
        callback?.Invoke(users);
    }


    public override void SelectUser(int id,Action<UserData> callback)
    {
        string query = $"select * from {ConnectionInfo.database}.users where  idUser = {id}";       
        MySqlCommand command = new MySqlCommand(query, con);
        try
        {
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                string name = reader.GetString("username");
                int scores = reader.GetInt32("userScore");
                int combo = reader.GetInt32("userCombo");
                UserData user = new(id, name, scores, combo);
                command.Dispose();
                callback?.Invoke(user);
            }
            else
            {
                command.Dispose();
            }
        }
        catch (System.Exception ex)
        {
            command.Dispose();
            Debug.LogError(ex.Message);
            
        }
    }

    public override void SelectUserByNick(string Name, Action<UserData> callback)
    {
        string query = $"select * from {ConnectionInfo.database}.users where  username = '{Name}'";
        MySqlCommand command = new MySqlCommand(query, con);
        try
        {
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                int id = reader.GetInt32("idUser");
                int scores = reader.GetInt32("userScore");
                int combo = reader.GetInt32("userCombo");
                UserData user = new(id, name, scores, combo);
                command.Dispose();
                callback?.Invoke(user);
            }
            else
            {
                command.Dispose();
            }
        }
        catch (System.Exception ex)
        {
            command.Dispose();
            Debug.LogError(ex.Message);

        }
    }

    #endregion
}




public class UserData
{  
    private int m_userId;
    public int UserId => m_userId;

    private string m_userName;
    public string UserName => m_userName;

    private int m_userScore;
    public int UserScore => m_userScore;

    private int m_userCombo;
    public int UserCombo => m_userCombo;

    public UserData(int userId, string userName, int userScore, int userCombo)
    {
        m_userId = userId;
        m_userName = userName;
        m_userScore = userScore;
        m_userCombo = userCombo;
    }
}