using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTextAlpha : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] float fadeInTime = 1;
    [SerializeField] float stayVisibleTime = 2;
    [SerializeField] float fadeOutTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        float t = 0;

        while(t<1)
        {
            t += Time.deltaTime / fadeInTime;

            text.alpha = t;

            yield return null;
        }

        yield return new WaitForSeconds(stayVisibleTime);

        t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / fadeOutTime;

            text.alpha = 1-t;

            yield return null;
        }

    }
}
