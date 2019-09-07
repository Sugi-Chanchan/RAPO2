using System.Collections;
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


        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible
        File.WriteAllBytes(Application.dataPath + "photo.png", bytes);



    }
}
