using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame 
{

    public float latitude;
    public float longitude;
    public float altitude;
    public float anglebasednorth;
    public Vector3 acceleration;
    public bool isactive;
    public string description;
    public Sprite sprite;


    public Frame(float Latitude, float Longitude,float Altitude, float Anglebasednorth, Vector3 Acceleration, bool Isactive ,string Description, Sprite Sprite) {

        latitude = Latitude;
        longitude = Longitude;
        altitude = Altitude;
        anglebasednorth = Anglebasednorth;
        acceleration = Acceleration;
        isactive = Isactive;
        description = Description;
        sprite = Sprite;

    }
}
