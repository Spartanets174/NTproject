using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserRow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerNumber;

    [SerializeField]
    private TextMeshProUGUI playerName;

    [SerializeField]
    private TextMeshProUGUI playerScores;

    public void SetData(int number, UserData userData, bool isCurrent)
    {
        playerNumber.text = "¹"+ number.ToString();
        playerName.text = userData.UserName;
        playerScores.text = userData.UserScore.ToString();

        if (isCurrent)
        {
            playerName.text += " (Âû)";
        }
    }
}
