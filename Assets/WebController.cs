using Renci.SshNet.Common;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class WebController : DataController
{
    public override void Init()
    {
        Debug.Log("Opened");
        connectionString = "http://127.0.0.1/Neiro/";
        
    }
    private void OnApplicationQuit()
    {
        if (playerData != null)
        {
            UpdatePlayerScore(null);
        }

    }
    private IEnumerator InsertToPlayersPOST(string Name, string link, Action<int> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerName", Name);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(link, form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                JSONArray jsonArray = JSON.Parse(webRequest.downloadHandler.text) as JSONArray;
                JSONObject jsonObject = jsonArray[0].AsObject;
                callback?.Invoke(jsonObject["idUser"]);
            }
        }
    }

    private IEnumerator UpdatePlayerScorePOST(string link, Action callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("myField", "myData");

        using (UnityWebRequest www = UnityWebRequest.Post(link, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("updated");
                callback?.Invoke();
            }
        }
    }

    private IEnumerator SelectUsersGET(string link, Action<List<UserData>> callback)
    {      
        using (UnityWebRequest webRequest = UnityWebRequest.Get(link))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = link.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError(pages[page] + ": ConnectionError: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    if (webRequest.downloadHandler.text != "0")
                    {
                        List<UserData> userDatas = new List<UserData>();
                        JSONArray jsonArray = JSON.Parse(webRequest.downloadHandler.text) as JSONArray;
                        for (int i = 0; i < jsonArray.Count; i++)
                        {
                            JSONObject jsonObject = jsonArray[i].AsObject;
                            userDatas.Add(new UserData(jsonObject["idUser"], jsonObject["username"], jsonObject["userScore"], jsonObject["userCombo"])) ;
                            callback?.Invoke(userDatas);
                        }                                           
                    }
                    break;
            }
        }
    }

    private IEnumerator SelectUserByNickGET(string nick, string link, Action<UserData> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerNick", nick);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(link,form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success|| webRequest.downloadHandler.text=="0")
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                JSONArray jsonArray = JSON.Parse(webRequest.downloadHandler.text) as JSONArray;
                JSONObject jsonObject = jsonArray[0].AsObject;
                UserData userData = new UserData(jsonObject["idUser"], jsonObject["username"], jsonObject["userScore"], jsonObject["userCombo"]);
                callback?.Invoke(userData);
            }
        }
    }

    private IEnumerator SelectUserGET(int id, string link, Action<UserData> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerId", id);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(link, form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success || webRequest.downloadHandler.text == "0")
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                JSONArray jsonArray = JSON.Parse(webRequest.downloadHandler.text) as JSONArray;
                JSONObject jsonObject = jsonArray[0].AsObject;
                UserData userData = new UserData(jsonObject["idUser"], jsonObject["username"], jsonObject["userScore"], jsonObject["userCombo"]);
                callback?.Invoke(userData);
            }
        }
    }

    public override void InsertToPlayers(string Name, Action<int> callback)
    {
        StartCoroutine(InsertToPlayersPOST(Name, $"{connectionString}InsertToPlayers.php", callback));
    }

    public override void UpdatePlayerScore(Action callback)
    {
        StartCoroutine(UpdatePlayerScorePOST($"{connectionString}UpdatePlayerScore.php", callback));
    }

    public override void SelectUsers(Action<List<UserData>> callback)
    {
        StartCoroutine(SelectUsersGET($"{connectionString}SelectUsers.php", callback));
    }

    public override void SelectUser(int id, Action<UserData> callback)
    {
        StartCoroutine(SelectUserGET(id,$"{connectionString}SelectUser.php", callback));
    }

    public override void SelectUserByNick(string Name, Action<UserData> callback)
    {
        StartCoroutine(SelectUserByNickGET(Name,$"{connectionString}SelectUserByNick.php", callback));
    }
}
