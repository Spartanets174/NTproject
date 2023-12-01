using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserRow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerNumberAndName;

    [SerializeField]
    private TextMeshProUGUI playerCombo;

    [SerializeField]
    private TextMeshProUGUI playerScores;

    public void SetData(int number, UserData userData, bool isCurrent)
    {
        playerNumberAndName.text = "#"+ number.ToString()+". "+ userData.UserName;
        playerCombo.text = userData.UserCombo.ToString();
        playerScores.text = userData.UserScore.ToString();

        if (isCurrent)
        {
            playerNumberAndName.text += " (Вы)";
        }
    }
}
