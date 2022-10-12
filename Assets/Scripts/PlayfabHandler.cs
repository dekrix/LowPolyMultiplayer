using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayfabHandler : MonoBehaviour
{
    public InputField emailLogin, passwordLogin;
    public InputField usernameRegister, passwordRegister, emailRegister, nameInput;
    public GameObject LoginRegisterPanel, invalidLoginCredentials, invalidRegistrationCredentials, usernamePanel;
    public Text usernameText;

    void Start()
    {
        Load.instance.LoadData();

        if(Load.email != null)
        {
            var request = new LoginWithEmailAddressRequest
            {
                Email = Load.email,
                Password = Load.password,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true,
                    GetUserAccountInfo = true
                }
            };

            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnErrorLogin);
        }
    }
  
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailLogin.text,
            Password = passwordLogin.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true,
                GetUserAccountInfo = true
            }
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnErrorLogin);
    }

    void OnErrorLogin(PlayFabError error)
    {
        invalidLoginCredentials.SetActive(false);
        invalidLoginCredentials.SetActive(true);
    }

    void OnLoginSuccess(LoginResult result)
    {
        print("Login successfull");
        Load.instance.LoadData();

        if(Load.email == null)
        {
            print("Data loaded");
            Load.email = emailLogin.text;
            Load.password = passwordLogin.text;
            Load.username = result.InfoResultPayload.PlayerProfile.DisplayName;
            SaveData();
        }

        LoginRegisterPanel.SetActive(false);
    }

    public void Register()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = usernameRegister.text,
            Email = emailRegister.text,
            Password = passwordRegister.text,
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccessfull, OnErrorRegister);
    }

    void OnRegisterSuccessfull(RegisterPlayFabUserResult result)
    {
        print("Register successfull");
        Load.instance.LoadData();
        Load.username = usernameRegister.text;
        Load.password = passwordRegister.text;
        Load.email = emailRegister.text;
        SetUsername();
        SaveData();
        LoginRegisterPanel.SetActive(false);
    }

    void OnErrorRegister(PlayFabError error)
    {
        invalidRegistrationCredentials.SetActive(false);
        invalidRegistrationCredentials.SetActive(true);
    }

    void SetUsername()
    {
        usernameText.text = Load.username.ToUpper();
    }

    public void SaveData()
    {
        SaveSystem.SaveData();
    }
}
