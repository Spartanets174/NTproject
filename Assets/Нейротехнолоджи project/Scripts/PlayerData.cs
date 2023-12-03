using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player data")]
public class PlayerData : ScriptableObject
{
    public int playerId;

    public string playerName;

    public int playerScores;

    public int playerCombo;

}
