using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;


using UnityEngine.UI;

public class judge : MonoBehaviour
{

    public Image image;

    private List<Frame> fres = new List<Frame>();
    public int time;

    public WebCamController webcamcontroller;
    public GameObject plane;

    Sprite nowsprite;
    Frame nowframe;

    bool nowframecompleted;

    int a;
    int b;


    private void Start()
    {

        webcamcontroller = plane.GetComponent<WebCamController>();
        InvokeRepeating("ListJudge", 1, time);
    }




    void ListJudge() {


        StartCoroutine("ListJudgeCoroutine");
        Input.location.Start();

    }


    IEnumerator ListJudgeCoroutine(){

        nowframecompleted = false; 

        yield return NowFrame();


        if (nowframecompleted) { } else { yield break; }


        fres.Add(nowframe);



        image.sprite = nowframe.sprite;




    }


    IEnumerator  NowFrame()
    {

        print(Input.location.status);
            
            var location = Input.location.lastData;
            if (location.longitude != 0)
            {


        }
        else
        {
           yield break;
        }



        yield return TakePhoto();



        nowframe = new Frame(location.latitude, location.longitude, location.altitude, Input.compass.trueHeading, Input.acceleration, false,"", nowsprite);



        nowframecompleted = true;



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
        Sprite sprite= Sprite.Create(photo, new Rect(0, 0, 256, 256), Vector2.zero);

        nowsprite = sprite;
        




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
