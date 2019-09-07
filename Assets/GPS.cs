using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GetLocation", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetLocation()
    {
        Input.location.Start();
        var location = Input.location.lastData.longitude;
        if (location != 0)
        {

            print(Input.location.lastData.latitude+":"+ Input.location.lastData.longitude);
        }
    }
}
