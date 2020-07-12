using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialStep : TutoListener
{
    public UnityEvent onStep = new UnityEvent();

    protected override void OnStep()
    {
        onStep.Invoke();
    }
}
