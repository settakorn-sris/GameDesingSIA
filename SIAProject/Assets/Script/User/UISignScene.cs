using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISignScene : MonoBehaviour
{
    public RectTransform signInUI;
    public RectTransform signUpUI;
    public RectTransform mainMenuUI;
    public void SignInComplete()
    {
        signInUI.gameObject.SetActive(false);
        OpenMenu();
    }
    public void TurnOffSignIn()
    {
        signInUI.gameObject.SetActive(false);
        signUpUI.gameObject.SetActive(true);
    }
    public void TurnOffSignUp()
    {
        signInUI.gameObject.SetActive(true);
        signUpUI.gameObject.SetActive(false);
    }
    public void OpenMenu()
    {
        mainMenuUI.gameObject.SetActive(true);
    }
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
