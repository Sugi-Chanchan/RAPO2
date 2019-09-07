using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;


using UnityEngine.UI;

public class judge : MonoBehaviour
{

    public Image image;
    public Text text;
    public Text test2;


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

        Input.gyro.enabled = true;
        Input.compass.enabled = true;



        StartCoroutine("ListJudgeCoroutine");
        if (Input.location.status != LocationServiceStatus.Running)
        {

        Input.location.Start();


        }

    }


    IEnumerator ListJudgeCoroutine(){

         nowframecompleted = false; 


        yield return NowFrame();


        if (nowframecompleted) { } else { yield break; }


        fres.Add(nowframe);



        image.sprite = nowframe.sprite;



        if (a - 10 > 0)
        {
            /*
            Vector2 before = new Vector2(fres[a - 7].longitude - fres[a - 10].longitude, fres[a - 7].latitude - fres[a - 10].latitude);

            Vector2 after = new Vector2(fres[a].longitude - fres[a-4].longitude, fres[a].latitude - fres[a - 4].latitude);


            var angles = Vector2.Angle(before, after);

            text.text = angles+"";
            test2.text = nowframe.longitude + ":" + nowframe.latitude;

    */

            /*
                        float before = (fres[a - 10].anglebasednorth + fres[a - 9].anglebasednorth + fres[a - 8].anglebasednorth + fres[a - 7].anglebasednorth) / 4;
                            float after =  (fres[a].anglebasednorth + fres[a - 1].anglebasednorth + fres[a - 2].anglebasednorth + fres[a - 3].anglebasednorth) / 4;

                        var angle = after - before;

                */

            Quaternion q = Input.gyro.attitude;

            //print(q.eulerAngles.y);
            test2.text = q.eulerAngles.x+":"+ q.eulerAngles.y+"*"+q.eulerAngles.z ;





        }







        a++;
        b++;
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
        //print(Input.compass.trueHeading);




       









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
        photo.Apply();
        photo = rotateTexture(photo, true);
        Sprite sprite= Sprite.Create(photo, new Rect(0, 0, 120, 160), Vector2.zero);

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
