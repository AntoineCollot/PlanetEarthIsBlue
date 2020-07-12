using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlanetColorPalette", order =0,fileName = "PlanetColorPalette")]
public class PlanetColorPalette : ScriptableObject
{
    public ColorPalette[] palette = null;
}

[System.Serializable]
public struct ColorPalette
{
    public Color[] colors;

    public Color GetRandomColor()
    {
        return colors[Random.Range(0, colors.Length)];
    }

    public Color GetMainColor()
    {
        return colors[0];
    }
}

