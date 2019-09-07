using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamController : MonoBehaviour
{

    //https://qiita.com/like_and_co/items/c760cdb1eee8b532af57
    int width = 1920;
    int height = 1080;
    int fps = 60;
    public WebCamTexture webcamTexture;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        webcamTexture = new WebCamTexture(devices[0].name, this.width, this.height, this.fps);

    

        
        GetComponent<Renderer>().material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
}
