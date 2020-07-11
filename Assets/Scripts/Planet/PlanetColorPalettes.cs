using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetColorPalettes : MonoBehaviour
{
    [SerializeField] ColorPalette[] palettes;

    static int currentPaletteSeed;
    public static PlanetColorPalettes Instance;

    public static Color RandomColor
    {
        get
        {
            return Instance.palettes[currentPaletteSeed].GetRandomColor();
        }
    }

    public static Color MainColor
    {
        get
        {
            return Instance.palettes[currentPaletteSeed].GetMainColor();
        }
    }

    void Awake()
    {
        Instance = this;
        currentPaletteSeed = Random.Range(0, Instance.palettes.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
