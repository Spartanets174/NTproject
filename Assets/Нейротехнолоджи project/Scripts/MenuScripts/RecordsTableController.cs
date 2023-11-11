using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecordsTableController : MonoBehaviour, IBootstrapper
{
    private DbManager dbManager;

    public DbManager DbManager => dbManager;

    private List<UserData> m_users = new();
    public List<UserData> Users => m_users;

    public void Init()
    {
        dbManager = FindObjectOfType<DbManager>();
        SortUsers();
    }

    public void UpdateUserScore()
    {
        dbManager.UpdatePlayerScore();
        SortUsers();
    }

    private void SortUsers()
    {
        m_users = dbManager.SelectUsers();
        m_users = m_users.OrderByDescending(u => u.UserScore).ToList();
    }
}
