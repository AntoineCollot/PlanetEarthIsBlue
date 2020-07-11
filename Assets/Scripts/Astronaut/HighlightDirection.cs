using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightDirection : MonoBehaviour
{
    [SerializeField] float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        transform.LookAt((Vector2)transform.position + direction, Vector3.forward);
    }
}
