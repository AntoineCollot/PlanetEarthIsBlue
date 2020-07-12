using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour
{
    [SerializeField] Transform astronaut=null;
    [SerializeField] Transform earth=null;
    [SerializeField] Vector2 margins = Vector2.zero;
    [SerializeField] float minDistance = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        Vector2 astronautPos = new Vector2(Random.Range(margins.x, 1f- margins.x), Random.Range(margins.y, 1f- margins.y));
        astronaut.position = (Vector2)cam.ViewportToWorldPoint(astronautPos);

        Vector2 randomPosition = astronautPos;
        //Find a position for the earth that isn't nearby
        while (Vector2.Distance(astronautPos, randomPosition)< minDistance)
        {
            randomPosition = new Vector2(Random.Range(margins.x, 1f - margins.x), Random.Range(margins.y, 1f - margins.y));
        }
        earth.position = (Vector2)cam.ViewportToWorldPoint(randomPosition);
    }
}
