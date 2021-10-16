using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISignScene : MonoBehaviour
{

    public GameObject signInUI;
    public GameObject signUpUI;

    public void TurnOffSignIn()
    {
        signInUI.SetActive(false);
        signUpUI.SetActive(true);
    }
    public void TurnOffSignUp()
    {
        signInUI.SetActive(true);
        signUpUI.SetActive(false);
    }
}
