using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCancelDragUntilStep : TutoListener
{
    new Rigidbody2D rigidbody;
    float originalDrag;
    float originalAngualarDrag;

    protected override void Start()
    {
        base.Start();

        rigidbody = GetComponent<Rigidbody2D>();
        originalDrag = rigidbody.drag;
        originalAngualarDrag = rigidbody.angularDrag;

        rigidbody.drag = 0;
        rigidbody.angularDrag = 0;
    }

    protected override void OnStep()
    {
        rigidbody.drag = originalDrag;
        rigidbody.angularDrag = originalAngualarDrag;
    }
}