using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawnEarth : MonoBehaviour
{
    [SerializeField] Transform astronaut = null;
    [SerializeField] Transform earth = null;
    [SerializeField] Vector2 margins = Vector2.zero;
    [SerializeField] float minDistance = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        Vector2 astronautViewportPos = cam.WorldToViewportPoint(astronaut.position);
        Vector2 earthViewportPos = cam.WorldToViewportPoint(earth.position);
        //Find a position for the earth that isn't nearby
        while (Vector2.Distance(astronautViewportPos, earthViewportPos) < minDistance)
        {
            earthViewportPos = new Vector2(Random.Range(margins.x, 1f - margins.x), Random.Range(margins.y, 1f - margins.y));
        }
        earth.position = (Vector2)Camera.main.ViewportToWorldPoint(earthViewportPos);
    }
}
