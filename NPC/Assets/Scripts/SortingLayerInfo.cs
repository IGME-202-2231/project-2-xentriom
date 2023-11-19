using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SortingLayerInfo
{
    private string layer;
    private float z;

    public string Layer { get { return layer; } }
    public float Z { get { return z; } }

    public SortingLayerInfo(string name, float zValue)
    {
        layer = name;
        z = zValue;
    }
}