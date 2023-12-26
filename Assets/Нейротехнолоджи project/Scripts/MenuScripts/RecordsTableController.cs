using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecordsTableController : MonoBehaviour, IBootstrapper
{
    private DataController dataController;

    public DataController DataController => dataController;

    private List<UserData> m_users = new();
    public List<UserData> Users => m_users;

    public void Init()
    {
        dataController = FindObjectOfType<DataController>();
        SortUsers();
    }

    public void UpdateUserScore()
    {
        dataController.UpdatePlayerScore(SortUsers);
    }

    private void SortUsers()
    {
        dataController.SelectUsers(
            (
                List<UserData> listUsers) =>
                {
                    m_users = listUsers.OrderByDescending(u => u.UserScore).ToList();
                }
            );
        
    }
}
