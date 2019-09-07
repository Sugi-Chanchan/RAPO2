using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class judge : MonoBehaviour
{

    private List<Frame> fres = new List<Frame>();
    public int time;


    private void Start()
    {
        InvokeRepeating("ListJudge", 1, time);
    }

    
    void ListJudge() {

        



    }



}
