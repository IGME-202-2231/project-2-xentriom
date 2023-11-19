using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnvLayers { Foreground, Midground, Farground, Background }
public struct SortingLayerInfo
{
    private EnvLayers layer;
    private float z;

    public EnvLayers Layer { get { return layer; } }
    public float Z { get { return z; } }

    public SortingLayerInfo(EnvLayers name, float zValue)
    {
        layer = name;
        z = zValue;
    }
}