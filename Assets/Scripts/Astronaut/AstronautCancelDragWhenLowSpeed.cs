using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautCancelDragWhenLowSpeed : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    float originalDrag;
    float originalAngularDrag;
    [SerializeField] float minVelocity = 1;
    [SerializeField] float minAngularVelocity = 25;

    private void FixedUpdate()
    {
        if(rigidbody.velocity.magnitude< minVelocity)
        {
            rigidbody.drag = 0;
        }
        else
        {
            rigidbody.drag = originalDrag;
        }

        if(Mathf.Abs(rigidbody.angularVelocity)< minAngularVelocity)
        {
            rigidbody.angularDrag = 0;
        }
        else
        {
            rigidbody.angularDrag = originalAngularDrag;
        }
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        originalDrag = rigidbody.drag;
        originalAngularDrag = rigidbody.angularDrag;
    }
}
