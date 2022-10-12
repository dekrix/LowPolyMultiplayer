using System;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string Username, Password, Email;

    public PlayerData()
    {
        //Player
        Username = Load.username;
        Password = Load.password;
        Email = Load.email;
    }
}
