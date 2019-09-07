using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;



public class takePhoto : MonoBehaviour
{

    //http://baba-s.hatenablog.com/entry/2017/12/26/210500
    //https://codeday.me/jp/qa/20190223/291286.html


    public WebCamController webcamcontroller;
    public GameObject plane;
    public Text text;


    void Start()
    {
        text.text= Application.dataPath;
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

        photo = rotateTexture(photo, true);
        photo.Apply();

        




        NativeGallery.SaveImageToGallery(photo, "GalleryTest", "My img {0}.png");








        /*
                yield return new WaitForEndOfFrame();

                var w = Screen.width;
                var h = Screen.height;
                var ss = new Texture2D(w, h, TextureFormat.RGB24, false);
                ss.ReadPixels(new Rect(0, 0, w, h), 0, 0);
                ss.Apply();

                NativeGallery.SaveImageToGallery(ss, "GalleryTest", "My img {0}.png");

        */




        /*
        WebCamTexture webcamtexture = webcamcontroller.webcamTexture;

        // NOTE - you almost certainly have to do this here:

        yield return new WaitForEndOfFrame();

        // it's a rare case where the Unity doco is pretty clear,
        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
        // be sure to scroll down to the SECOND long example on that doco page 

        Texture2D photo = new Texture2D(webcamtexture.width, webcamtexture.height);
        / photo.SetPixels(webcamtexture.GetPixels());
          photo.Apply();

          //Encode to a PNG
          byte[] bytes = photo.EncodeToPNG();
          //Write out the PNG. Of course you have to substitute your_path for something sensible
         File.WriteAllBytes(Application.dataPath + "photo.png", bytes);
         */
        //   NativeGallery.SaveImageToGallery(photo, "rapo", "My img {10}.jpg");
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
