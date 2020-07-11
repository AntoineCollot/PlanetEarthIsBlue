using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPosition : MonoBehaviour
{
    public static CursorPosition Instance;
    Camera cam;

    public static Vector2 worldPosition;

    public static Vector2 viewportPosition;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        worldPosition = Instance.cam.ScreenToWorldPoint(Input.mousePosition);
        viewportPosition = Instance.cam.ScreenToViewportPoint(Input.mousePosition);
    }
}
