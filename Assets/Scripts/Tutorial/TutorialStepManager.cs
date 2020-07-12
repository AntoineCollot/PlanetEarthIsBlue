using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialStepManager : MonoBehaviour
{
    public enum Step { None,Empty,TextRelax, TextNothingYouCanDo, CreatePlanet, DestroyPlanet, CreateLargePlanet, Earth}

    public class TutoEvent : UnityEvent<Step> { }
    public TutoEvent onNewStep = new TutoEvent();

    public static TutorialStepManager Instance;
    public TutoStep[] steps = new TutoStep[0];

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayTuto());
    }

    IEnumerator PlayTuto()
    {
        int currentStep = 0;

        while(currentStep<steps.Length)
        {
            yield return StartCoroutine(PlayStep(currentStep));

            currentStep++;
        }
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Label(new Rect(10,100,200,200),"Time : " + Time.time.ToString("N0"));
    }
#endif

    IEnumerator PlayStep(int id)
    {
        Debug.Log("Step : " + steps[id].step);
        onNewStep.Invoke(steps[id].step);

        yield return new WaitForSeconds(steps[id].timeLength);
    }

    [System.Serializable]
    public struct TutoStep
    {
        public float timeLength;
        public Step step;
    }
}
