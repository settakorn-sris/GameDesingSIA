using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private Slider loadingSlider;
    private SoundManager soundManager;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        soundManager = SoundManager.Instance;
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadingCanvas.SetActive(true);
        soundManager.CountrolSoundBGMVolume(0);
        do
        {
            await Task.Delay(100);
            loadingSlider.value = scene.progress;
        }
        while (scene.progress < 0.9f);

        await Task.Delay(100);

        scene.allowSceneActivation = true;
        //loadingCanvas.SetActive(false);
    }
  
}
