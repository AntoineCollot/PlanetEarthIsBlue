using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutoListener : MonoBehaviour
{
    [SerializeField] protected TutorialStepManager.Step step = TutorialStepManager.Step.None;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        TutorialStepManager.Instance.onNewStep.AddListener(OnNewTutoStep);
    }

    protected void OnNewTutoStep(TutorialStepManager.Step newStep)
    {
        if (step == newStep)
            OnStep();
    }

    abstract protected void OnStep();
}
