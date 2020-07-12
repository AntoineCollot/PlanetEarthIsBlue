using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public enum Mode { Earth, Relax,Tutorial}
    [SerializeField] Mode mode = Mode.Earth;

    public static Mode currentMode;

    public static bool AllowDestructionWhenAttracting
    {
        get
        {
            switch (currentMode)
            {
                case Mode.Earth:
                    return false;
                case Mode.Tutorial:
                case Mode.Relax:
                default:
                    return true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentMode = mode;
    }
}
