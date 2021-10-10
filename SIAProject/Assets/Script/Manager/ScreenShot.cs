using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.Events;

public class ScreenShot : MonoBehaviour
{
    public int CaptureHeigh = Screen.height;
    public int CaptureWidth = (Screen.height / 16) * 9;

    public enum Format { RAW,JPG,PNG,PPM}
    public Format format = Format.JPG;

    private string outputFolder;

    private Rect react;
    private RenderTexture renderTexture;
    private Texture2D screenShot;

    private byte[] currentTexture;
    public bool IsProcessing;
    public Image ShowImage;
    public UnityEvent OnShowImage;

    

    // Start is called before the first frame update
    void Start()
    {
        outputFolder = Application.persistentDataPath + "/ScreenShots/";
        if(!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
            Debug.Log(outputFolder);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private string CreateFile(int width,int height)
    {

        string timeStamp = DateTime.Now.ToString("ddMMyyyyTHHmmss");
        string fileName = string.Format("{0}/screen_{1}x{2}_{3}.{4}", outputFolder, width, height, timeStamp, format.ToString().ToLower());
        return fileName;
    }

    private void CaptureScreen()
    {
        IsProcessing = true;
        if(renderTexture == null)
        {
            react = new Rect(0, 0, CaptureWidth, CaptureHeigh);
            renderTexture = new RenderTexture(CaptureWidth, CaptureHeigh, 24);
            screenShot = new Texture2D(CaptureWidth, CaptureHeigh, TextureFormat.RGB24, false);
        }
        Camera cameraMain = Camera.main;
        cameraMain.targetTexture = renderTexture;
        cameraMain.Render();

        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(react, 0, 0);

        cameraMain.targetTexture = null;
        RenderTexture.active = null;

        string fileName = CreateFile((int)react.width, (int)react.height);

        byte[] fileHeader = null;
        byte[] fileData = null;

        switch(format)
        {
            case Format.RAW:
                fileData = screenShot.GetRawTextureData();
                break;
            case Format.JPG:
                fileData = screenShot.EncodeToJPG();
                break;
            case Format.PNG:
                fileData = screenShot.EncodeToPNG();
                break;
            case Format.PPM:
                string headerString = string.Format("PG\n{0} {1}\n255\n", react.width, react.height);
                fileHeader = System.Text.Encoding.ASCII.GetBytes(headerString);
                fileData = screenShot.GetRawTextureData();
                break;
        }
        currentTexture = fileData;
        new System.Threading.Thread(() =>
        {
            FileStream file = File.Create(fileName);

            if (fileHeader != null)
            {
                file.Write(fileHeader, 0, fileHeader.Length);

            }
            file.Write(fileData, 0, fileData.Length);
            file.Close();

            Debug.Log(string.Format("Screenshot Saved {0}, size {1}", fileName, fileData.Length));
            IsProcessing = true;
        }).Start();

        StartCoroutine(ShowImg());
        Destroy(renderTexture);
        renderTexture = null;
        screenShot = null;
   
    }

    public void TakeScreenShot()
    {
        if(!IsProcessing)
        {
            CaptureScreen();
        }
        else
        {
            print("Currently Processing");
        }
    }
    public IEnumerator ShowImg()
    {
        yield return new WaitForEndOfFrame();
        ShowImage.material.mainTexture = null;
        Texture2D texture = new Texture2D(CaptureWidth, CaptureHeigh, TextureFormat.RGB24, false);
        texture.LoadImage(currentTexture);
        ShowImage.material.mainTexture = texture;

        OnShowImage?.Invoke();
    }

}
