using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public static Load instance;
    public static string username, password, email;

    void Start()
    {
        instance = this;
    }

    public void LoadAll()
    {
        LoadData();
    }

    public void SaveData()
    {
        SaveSystem.SaveData();
    }

    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadData();

        username = data.Username;
        password = data.Password;
        email = data.Email;
    }
}
