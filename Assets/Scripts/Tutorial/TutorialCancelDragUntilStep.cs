using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCancelDragUntilStep : TutoListener
{
    new Rigidbody2D rigidbody;
    float originalDrag;
    float originalAngularDrag;

    protected override void Start()
    {
        base.Start();

        rigidbody = GetComponent<Rigidbody2D>();
        originalDrag = rigidbody.drag;
        originalAngularDrag = rigidbody.angularDrag;

        rigidbody.drag = 0;
        rigidbody.angularDrag = 0;
    }

    protected override void OnStep()
    {
        rigidbody.drag = originalDrag;
        rigidbody.angularDrag = originalAngularDrag;

        GetComponent<AstronautCancelDragWhenLowSpeed>().enabled = true;
    }
}