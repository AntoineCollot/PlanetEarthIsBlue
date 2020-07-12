using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetColorManager : MonoBehaviour
{
    [SerializeField] PlanetColorPalette palettes =null;

    static int currentPaletteSeed;
    public static PlanetColorManager Instance;

    public static Color RandomColor
    {
        get
        {
            return Instance.palettes.palette[currentPaletteSeed].GetRandomColor();
        }
    }

    public static Color MainColor
    {
        get
        {
            return Instance.palettes.palette[currentPaletteSeed].GetMainColor();
        }
    }

    void Awake()
    {
        Instance = this;
        currentPaletteSeed = Random.Range(0, Instance.palettes.palette.Length);

        Debug.Log("Palette " + currentPaletteSeed + " selected");
    }

    public static Color GetColor(int id)
    {
        return Instance.palettes.palette[currentPaletteSeed].colors[id];
    }
}