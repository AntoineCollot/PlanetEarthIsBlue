using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPreview : MonoBehaviour
{
    public float radius;
    public float mass;
    public float gravityRadius;
    [SerializeField] int samples = 40;
    LineRenderer line;

    [Header("Audio")]
    [SerializeField] float audioEvolutionMultiplier = 1;
    new AudioSource audio;
    float lastRadius;
    float baseVolume;

    public bool PositionIsInGravityArea(Vector2 pos)
    {
        return Vector2.Distance(transform.position, pos) < gravityRadius;
    }

    // Start is called before the first frame update
    void Awake()
    {
        line = GetComponent<LineRenderer>();
        audio = GetComponent<AudioSource>();
        baseVolume = audio.volume;
        line.positionCount = samples;
    }

    private void OnEnable()
    {
        audio.Play();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        line.SetPositions(ComputeCirclePoints(radius, samples));

        //Audio
        audio.volume = Mathf.Lerp(0, baseVolume, Mathf.Abs(lastRadius - radius) / Time.deltaTime * audioEvolutionMultiplier);

        lastRadius = radius;
    }

    Vector3[] ComputeCirclePoints(float radius, int samples)
    {
        Vector3[] points = new Vector3[samples];
        for (int i = 0; i < samples; i++)
        {
            float angle = (2 * Mathf.PI * i) / samples;
            points[i] = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0) * radius;
        }

        return points;
    }
}
