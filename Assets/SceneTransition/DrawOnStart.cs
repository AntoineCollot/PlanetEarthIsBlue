using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOnStart : MonoBehaviour
{
    public float drawTime;

    [SerializeField]
    Material mat = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Draw());
    }

    IEnumerator Draw()
    {
        PostProcessDrawColor postProcess = GetComponent<PostProcessDrawColor>();
        Material originalMat = postProcess.material;
        postProcess.material = mat;
        float t = 0;

        while(t<1)
        {
            t += Time.deltaTime / drawTime;
            postProcess.intensity = Mathf.Lerp(1, 0, t);

            yield return null;
        }
        postProcess.material = originalMat;
    }
}
