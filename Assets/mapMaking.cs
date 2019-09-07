using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapMaking : MonoBehaviour
{

    Vector3 vector3;
    //public float[,] vs =new float[5,3] { { 2, 3, 2 }, { 3, 3, 2 }, { 4, 3, 2 }, { 4, 2, 2 }, { 4, 1, 2 } };
    
    public GameObject ball;
    public GameObject LINE;
    LineRenderer lineRenderer;
    public Frame Frame;
    public GameObject sp;
    Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = sp.GetComponent<Sprite>();

        List<Frame> frames = new List<Frame>();
        frames.Add(new Frame(2, 3, 2, 0, new Vector3(0, 0, 0), true, "", sprite));
        frames.Add(new Frame(3, 3, 2, 0, new Vector3(0, 0, 0), true, "", sprite));
        frames.Add(new Frame(4, 3, 2, 0, new Vector3(0, 0, 0), true, "", sprite));
        frames.Add(new Frame(4, 2, 2, 0, new Vector3(0, 0, 0), true, "", sprite));
        frames.Add(new Frame(4, 1, 2, 0, new Vector3(0, 0, 0), true, "", sprite));
        lineRenderer = LINE.GetComponent<LineRenderer>();

        makeMap(frames);
        //makeMap(Data.frames);
       
    }

    private void makeMap(List<Frame> frames)
    {
        Debug.Log(lineRenderer.positionCount);
        lineRenderer.positionCount = frames.Count;
        for (int i = 0; i < frames.Count; i++)
        {

            vector3.x = frames[i].latitude;
            vector3.y = frames[i].altitude;
            vector3.z = frames[i].longitude;
            if (frames[i].isactive)
            {
                Instantiate(ball, vector3, Quaternion.identity);

            }
 
            //lineRenderer.positionCount = i;
            lineRenderer.SetPosition(i, vector3);
        }
        
        
        
        
        /*
        for (int i = 0; i < vs.Length; i++)
        {

            vector3.x = vs[i,1];
            vector3.y = vs[i,0];
            vector3.z = vs[i,2];
            Instantiate(ball, vector3, Quaternion.identity);
                }
                */
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
