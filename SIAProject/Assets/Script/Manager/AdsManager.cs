using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdsManager : MonoBehaviour,IUnityAdsListener
{
    private string gameID = "4417043";
    public bool testMode = true;
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = SoundManager.Instance;
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, testMode);
    }
    

    public void OpenAds(string placement)
    {
        soundManager.Play(soundManager.AudioSorceForPlayerAction, SoundManager.Sound.PUSH_BUTTON);
        Advertisement.Show(placement);
    }

    public void OnUnityAdsReady(string placementId)
    {
        print("Ready"+placementId);
    }

    public void OnUnityAdsDidError(string message)
    {
        print("Error"+message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        print("Start"); 
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            GameManager.Instance.CountrolAdsPanel(true);
            GameManager.Instance.HealingWithAds();

        }
        else
        {
            LoadSceneManager.Instance.LoadScene("MainMenu");
            GameManager.Instance.CountrolAdsPanel(false);
        }
    }
}
