using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    public Slider volumeSlider;
    public Text usernameText;
    public InputField usernameInputField;
    public Sprite[] icons;
    public Image profileImage, profileButtonIcon;
    public static Profile instance;
    public Animator iconsAnimator;

    public int iconChosen;

    bool iconsOpened;

    void Start()
    {
        instance = this;   
    }

    public void SaveButton()
    {
        PlayfabHandler.instance.SetPlayerData(volumeSlider.value, iconChosen, usernameInputField.text);
    }

    public void SetIcon(int id)
    {
        print("Clicked");
        iconChosen = id;
        iconsAnimator.SetTrigger("Close");
        profileButtonIcon.sprite = icons[id];
        iconsOpened = !iconsOpened;
    }

    public void OpenIcons()
    {
        print(iconsOpened);
        if (iconsOpened)
        {
            iconsAnimator.SetTrigger("Close");
        }
        else
        {
            iconsAnimator.SetTrigger("Open");
        }
        iconsOpened = !iconsOpened;
    }
}
