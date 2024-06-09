using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Vector3Double
{
    public double x;
    public double y;
    public double z;


    public Vector3Double(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    // Converting from the double, to normal unity vector3
    public Vector3 ToVector3()
    {
        return new Vector3((float)x, (float)y, (float)z);
    }


}
