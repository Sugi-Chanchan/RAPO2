using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class takePhoto : MonoBehaviour
{
    public WebCamTexture webCamTexture;
    public GameObject plane;
    public Text text;


    void Start()
    {
        text.text= Application.dataPath;
        webCamTexture = plane.GetComponent<WebCamController>().webcamTexture;

    }


    public void Take()
    {

        StartCoroutine("TakePhoto");
    }

     IEnumerator TakePhoto()
    {

        // NOTE - you almost certainly have to do this here:

        yield return new WaitForEndOfFrame();

        // it's a rare case where the Unity doco is pretty clear,
        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
        // be sure to scroll down to the SECOND long example on that doco page 

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible
        File.WriteAllBytes(Application.dataPath + "photo.png", bytes);
    }
}
