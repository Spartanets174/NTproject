using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordsTablePresenter : MonoBehaviour, IBootstrapper
{

    [SerializeField]
    private Button closeButton;

    [SerializeField]
    private GameObject Table;

    [SerializeField]
    private Transform parentToSpawn;

    [SerializeField]
    private UserRow userRow;

    private RecordsTableController recordsTableController;
    private PlayerData playerData;

    private List<GameObject> recordsList=new();
    public void Init()
    {
        closeButton.onClick.AddListener(TurnOffTable);

        recordsTableController =FindObjectOfType<RecordsTableController>();
        playerData = recordsTableController.DbManager.PlayerData;
    }
    private void OnEnable()
    {
        recordsTableController.UpdateUserScore();
        SpawnRows();
    }
    private void SpawnRows()
    {
        foreach (var item in recordsList)
        {
            Destroy(item);
        }

        recordsList.Clear();

        int count = 0;
        bool isCurrent = false;
        foreach (var user in recordsTableController.Users)
        {
            count++;
            UserRow row = Instantiate(userRow, Vector3.zero, Quaternion.identity, parentToSpawn);
            if (playerData.playerId == user.UserId)
            {
                isCurrent = true;
            }
            else
            {
                isCurrent = false;
            }
            row.SetData(count, user, isCurrent);

            recordsList.Add(row.gameObject);
        }
    }

    private void TurnOffTable()
    {
        Table.SetActive(false);
    }


}
