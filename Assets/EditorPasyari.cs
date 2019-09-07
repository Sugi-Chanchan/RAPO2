﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;



public class EditorPasyari : MonoBehaviour
{

    //http://baba-s.hatenablog.com/entry/2017/12/26/210500
    //https://codeday.me/jp/qa/20190223/291286.html


    public WebCamController webcamcontroller;
    public GameObject plane;


    void Start()
    {
        webcamcontroller = plane.GetComponent<WebCamController>();

    }


    public void Take()
    {

        StartCoroutine("TakePhoto");
    }

    IEnumerator TakePhoto()
    {
        WebCamTexture webcamtexture = webcamcontroller.webcamTexture;

        yield return new WaitForEndOfFrame();

        // it's a rare case where the Unity doco is pretty clear,
        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
        // be sure to scroll down to the SECOND long example on that doco page 

        Texture2D photo = new Texture2D(webcamtexture.width, webcamtexture.height);


        yield return new WaitForEndOfFrame();

        photo.SetPixels(webcamtexture.GetPixels());


        photo.Apply();

        photo = rotateTexture(photo,true);

        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible
        File.WriteAllBytes(Application.dataPath + "photo.png", bytes);



    }

    Texture2D rotateTexture(Texture2D originalTexture, bool clockwise)
    {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iRotated, iOriginal;

        for (int j = 0; j < h; ++j)
        {
            for (int i = 0; i < w; ++i)
            {
                iRotated = (i + 1) * h - j - 1;
                iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                rotated[iRotated] = original[iOriginal];
            }
        }

        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();
        return rotatedTexture;
    }
}
