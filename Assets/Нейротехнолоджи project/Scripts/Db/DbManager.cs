using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public static class ConnectionInfo
{
    public static string ip = "127.0.0.1";
    public static string uid = "root";
    public static string pwd = "12345";
    public static string database = "dbneiro";
}

public class DbManager : MonoBehaviour
{
    static string connectionString = $"server = {ConnectionInfo.ip}; uid = {ConnectionInfo.uid}; pwd = {ConnectionInfo.pwd}; Database = {ConnectionInfo.database}; SSLMode = none";

    public static MySqlConnection con;

    [SerializeField] private PlayerData playerData;
    public PlayerData PlayerData => playerData;
    public void Awake()
    {
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
            UpdatePlayerScore();

            closeCon();
            Debug.Log("closed");
        }
        
    }
    public void closeCon()
    {
        con.Close();
    }

    #region Player
    public int InsertToPlayers(string Name)
    {
        string query = $"insert into {ConnectionInfo.database}.users (username,userScore) values ('{Name}',{0})";

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
        return Convert.ToInt32(command.LastInsertedId);
    }

    public int UpdatePlayerScore()
    {
        string query = $"UPDATE {ConnectionInfo.database}.users SET userScore = {playerData.playerScores} where username='{playerData.playerName}'";

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
        return Convert.ToInt32(command.LastInsertedId);
    }
    public List<UserData> SelectUsers()
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
        return users;
    }


    public UserData SelectUser(int id)
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
                return user;
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
        return null;
    }

    public UserData SelectUserByNick(string Name)
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
                return user;
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
        return null;
    }

    #endregion
}




public class UserData
{
    [SerializeField]
    private int m_userId;
    public int UserId => m_userId;
    [SerializeField]
    private string m_userName;
    public string UserName => m_userName;

    [SerializeField]
    private int m_userScore;
    public int UserScore => m_userScore;

    [SerializeField]
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