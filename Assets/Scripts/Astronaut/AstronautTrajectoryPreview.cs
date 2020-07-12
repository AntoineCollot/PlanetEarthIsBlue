using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautTrajectoryPreview : MonoBehaviour
{
    public float previewTime = 3;
    public int pointCount = 10;
    new Rigidbody2D rigidbody;
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlanetSpawner.Instance.isSpawningPlanet)
        {
            line.enabled = true;
            Vector3[] points = ComputePreviewPoints();
            line.positionCount = pointCount + 1;
            line.SetPositions(points);
        }
        else
        {
            line.enabled = false;
        }
    }

    Vector3[] ComputePreviewPoints()
    {
        Vector2 position = rigidbody.position;
        Vector2 velocity = rigidbody.velocity;

        Vector3[] points = new Vector3[pointCount+1];
        float elapsedTime = 0;

        points[0] = position;
        while (elapsedTime<previewTime)
        {
            //Update the velocity
            List<PlanetGravity> planets = PlanetGravity.GetAllPlanetsApplyingGravity(position);
            foreach(PlanetGravity planet in planets)
            {
                planet.AngleGravityPull(position, ref velocity, out float torque);
            }

            if(PlanetSpawner.Instance.planetPreview.PositionIsInGravityArea(position))
            {
                Vector2 previewPosition = PlanetSpawner.Instance.planetPreview.transform.position;
                float previewMass = PlanetSpawner.Instance.planetPreview.mass;
                PlanetGravity.EmulatePlanetGravity(previewPosition, previewMass, position, ref velocity,out bool isBoosting, out float torque);
            }

            velocity = velocity * (1 - Time.fixedDeltaTime * rigidbody.drag);
            //max velocity
            if(velocity.magnitude>Physics2D.maxTranslationSpeed / Time.fixedDeltaTime)
            {
                velocity = velocity.normalized * Physics2D.maxTranslationSpeed / Time.fixedDeltaTime;
            }

            position += velocity * Time.fixedDeltaTime;


            int currentId = Mathf.CeilToInt((elapsedTime / previewTime) * pointCount);
            points[currentId] = position;

            elapsedTime += Time.fixedDeltaTime;
        }

        return points;
    }
}
