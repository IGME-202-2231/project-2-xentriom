using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnvLayers
{
    Foreground = 0,
    Midground = 5,
    Farground = 10,
    Background = 15
}

public struct SortingLayerInfo
{
    private EnvLayers layer;

    public readonly EnvLayers Layer => layer;
    public readonly float Z => (int)layer;

    public SortingLayerInfo(EnvLayers name)
    {
        layer = name;
    }
}