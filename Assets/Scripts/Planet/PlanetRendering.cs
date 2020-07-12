using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRendering : MonoBehaviour
{
    [SerializeField, Range(0, 1)] float radius = 0.75f;
    [SerializeField] bool randomizeAtStart = true;

    [Header("Shared Parameters")]
    [SerializeField, Range(0, 1)] float minStaturationShift = 0.3f;
    [SerializeField, Range(0, 1)] float randomValue = 0.3f;
    float baseFrequency;
    [SerializeField, Range(0, 1)] float randomFrequency = 0.3f;
    [SerializeField] float minAspect = 0.3f;
    [SerializeField] float maxAspect = 2.5f;
    [SerializeField] float minThresholdWidth = 0.15f;
    [SerializeField] float maxThresholdWidth = 0.75f;
    Material instancedMaterial;
    PlanetGravity planet;
    Animator anim;

    public const float PLANET_SIZE_MULTIPLIER = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        planet = GetComponent<PlanetGravity>();
        instancedMaterial = GetComponent<SpriteRenderer>().material;
        anim = GetComponent<Animator>();
        baseFrequency = instancedMaterial.GetFloat("_NoiseFreq");

        if (randomizeAtStart)
        {
            SetPlanetColors();
            SetPlanetNoise();
        }
        UpdatePlanetSize();
    }

    //// Update is called once per frame
    void Update()
    {
        instancedMaterial.SetFloat("_CircleRadius", radius);

        if(anim!=null)
            anim.SetBool("IsAttracting", planet.isAttractingSomething);
    }

    void SetPlanetNoise()
    {
        float frequency = Random.Range(baseFrequency - randomFrequency, baseFrequency + randomFrequency);
        instancedMaterial.SetFloat("_NoiseFreq", frequency);

        float aspect = Random.Range(minAspect, maxAspect);
        instancedMaterial.SetFloat("_NoiseAspect", aspect);

        float thresholdValue = Random.Range(0f, 1f);
        instancedMaterial.SetFloat("_ThresholdValue", thresholdValue);

        float thresholdWidth = Random.Range(minThresholdWidth, maxThresholdWidth);
        instancedMaterial.SetFloat("_ThresholdWidth", thresholdWidth);
    }

    void SetPlanetColors()
    {
        Color mainColor = PlanetColorManager.RandomColor;

        Color secondaryColor = mainColor;
        Color.RGBToHSV(secondaryColor, out float h, out float s, out float v);

        s = Random.Range(s - minStaturationShift, s + minStaturationShift);
        v = Random.Range(v - randomValue, v + randomValue);

        s = Mathf.Clamp(s,0.2f, 0.8f);
        v = Mathf.Clamp(v, 0.2f, 0.8f);

        secondaryColor = Color.HSVToRGB(h, s, v);

        instancedMaterial.SetColor("_Color1", mainColor);
        instancedMaterial.SetColor("_Color2", secondaryColor);
    }

    void UpdatePlanetSize()
    {
        transform.localScale = Vector3.one * planet.radius * 2 * 1 / PLANET_SIZE_MULTIPLIER;
    }
}
