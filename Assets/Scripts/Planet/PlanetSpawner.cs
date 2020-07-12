using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] PlanetGravity planetPrefab = null;
    [SerializeField] float minPlanetRadius = 1.5f;
    [SerializeField] float maxPlanetRadius = 5f;
    [SerializeField] float minGravity = 5;
    [SerializeField] float maxGravity = 10;


    [Header("Planet Preview")]
    public PlanetPreview planetPreview = null;
    Vector2 worldMouseButtonDownPosition;
    float refSize;
    public float previewSmooth = 0.1f;

    public bool isSpawningPlanet;
    public static PlanetSpawner Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !PlanetGravity.CursorIsInAnyGravityArea)
        {
            StartCoroutine(SpawnPlanetProcess());
        }
        if(Input.GetMouseButtonDown(1))
        {
            isSpawningPlanet = false;
            planetPreview.gameObject.SetActive(false);
            StopAllCoroutines();
        }
    }

    IEnumerator SpawnPlanetProcess()
    {
        isSpawningPlanet = true;
        worldMouseButtonDownPosition = CursorPosition.worldPosition;
        planetPreview.transform.position = worldMouseButtonDownPosition;
        planetPreview.radius = 0;
        refSize = 0;
        planetPreview.gameObject.SetActive(true);

        while(Input.GetMouseButton(0))
        {
            float targetRadius = ComputePlanetRadius(true, true);
            float relativeRadius = (targetRadius - minPlanetRadius) / (maxPlanetRadius - minPlanetRadius);
            planetPreview.radius = Mathf.SmoothDamp(planetPreview.radius, targetRadius, ref refSize, previewSmooth);
            planetPreview.mass = Mathf.Lerp(minGravity, maxGravity, relativeRadius);
            planetPreview.gravityRadius = planetPrefab.gravityRadius;

            yield return null;
        }

        planetPreview.gameObject.SetActive(false);
        SpawnPlanet();
        isSpawningPlanet = false;
    }

    //void UpdatePlanetPreview()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        worldMouseButtonDownPosition = CursorPosition.worldPosition;
    //        planetPreview.transform.position = worldMouseButtonDownPosition;
    //        planetPreview.radius = 0;
    //    }

    //    if (Input.GetMouseButton(0))
    //    {
    //        planetPreview.gameObject.SetActive(true);

    //        planetPreview.radius = Mathf.SmoothDamp(planetPreview.radius, ComputePlanetRadius(true, true), ref refSize, previewSmooth);
    //    }
    //    else
    //    {
    //        planetPreview.gameObject.SetActive(false);
    //    }
    //}

    void SpawnPlanet()
    {
        PlanetGravity planet = Instantiate(planetPrefab, worldMouseButtonDownPosition, Quaternion.identity, transform);
        planet.radius = ComputePlanetRadius(true, true);
        float relativeRadius = (planet.radius - minPlanetRadius) / (maxPlanetRadius - minPlanetRadius);
        planet.mass = Mathf.Lerp(minGravity, maxGravity, relativeRadius);
    }

    float ComputePlanetRadius(bool applyMinSize, bool applyMaxSize)
    {
        float radius = Vector2.Distance(worldMouseButtonDownPosition, CursorPosition.worldPosition);

        if (applyMinSize)
            radius = Mathf.Max(minPlanetRadius, radius);

        if (applyMaxSize)
            radius = Mathf.Min(maxPlanetRadius, radius);

        return radius;
    }
}
