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
    public GameObject LoginRegisterPanel, invalidLoginCredentials, invalidRegistrationCredentials, profilePanel;
    public Text usernameText;
    public static PlayfabHandler instance;

    string myID;

    void Start()
    {
        instance = this;
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
        myID = result.PlayFabId;
        GetPlayerData();
        profilePanel.SetActive(false);
        LoginRegisterPanel.SetActive(false);
    }

    public void Register()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = usernameRegister.text,
            DisplayName = usernameRegister.text,
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
        myID = result.PlayFabId;
        GetPlayerData();
        profilePanel.SetActive(false);
        LoginRegisterPanel.SetActive(false);
    }

    void OnErrorRegister(PlayFabError error)
    {
        invalidRegistrationCredentials.SetActive(false);
        invalidRegistrationCredentials.SetActive(true);
    }

    public void GetPlayerData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myID,
            Keys = null
        }, PlayerDataSuccess, OnError);
    }

    void PlayerDataSuccess(GetUserDataResult result)
    {
        print("Getting");
        if(result.Data != null)
        {
            if (result.Data.ContainsKey("Volume"))
            {
                float value = float.Parse(result.Data["Volume"].Value);
                Profile.instance.volumeSlider.value = value;
            }

            if (result.Data.ContainsKey("IconID"))
            {
                int value = int.Parse(result.Data["IconID"].Value);
                Profile.instance.iconChosen = value;
                Profile.instance.profileButtonIcon.sprite = Profile.instance.icons[value];
                Profile.instance.profileImage.GetComponent<Image>().sprite = Profile.instance.icons[value];
            }

            if (result.Data.ContainsKey("Username"))
            {
                string value = result.Data["Username"].Value;
                Profile.instance.usernameInputField.text = value;
                Profile.instance.usernameText.text = value;
            }
        }
    }

    void OnError(PlayFabError error)
    {

    }

    public void SetPlayerData(float volume, int iconID, string username)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"Volume", volume.ToString() },
                {"IconID", iconID.ToString() },
                {"Username", username }
            }
        },SetDataSuccess, OnError);
    }

    void SetDataSuccess(UpdateUserDataResult result)
    {
        GetPlayerData();
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
