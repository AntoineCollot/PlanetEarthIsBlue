using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float radius;
    public float mass = 1;
    public float gravityRadius = 10;
    [SerializeField] LayerMask astronautLayer = 1 << 8;

    public bool isAttractingSomething { get; private set; }
    public bool isBoostingSomething;

    public const float VELOCITY_BOOST = 5;
    public const float RESULTING_TORQUE_MULT = 0.03f;
    public const float G = 9.81f;
        
    public static List<PlanetGravity> allPlanets = new List<PlanetGravity>();

    public bool CursorIsInGravityArea
    {
        get
        {
            return PositionIsInGravityArea(CursorPosition.worldPosition);
        }
    }

    public static bool CursorIsInAnyGravityArea
    {
        get
        {
            foreach(PlanetGravity planet in allPlanets)
            {
                if (planet.CursorIsInGravityArea)
                    return true;
            }
            return false;
        }
    }

    public static bool AnyPlanetBoostingSomething
    {
        get
        {
            foreach (PlanetGravity planet in allPlanets)
            {
                if (planet.isBoostingSomething)
                    return true;
            }
            return false;
        }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        allPlanets.Add(this);
    }

    void OnDisable()
    {
        allPlanets.Remove(this);
    }

    private void FixedUpdate()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, gravityRadius, astronautLayer);
        isAttractingSomething = false;

        foreach (Collider2D hit in hitObjects)
        {
            isAttractingSomething = true;
            Rigidbody2D other = hit.attachedRigidbody;
            Vector2 velocity = other.velocity;
            Vector2 position = other.position;
            AngleGravityPull(position, ref velocity, out float torque);
            other.AddTorque(torque);
            other.velocity = velocity;
        }
    }

    //void RealGravityPull(Rigidbody2D other)
    //{
    //    float sqrtDist = ((Vector2)transform.position - other.position).sqrMagnitude;
    //    Vector2 forceDirection = ((Vector2)transform.position - other.position).normalized;
    //    Vector2 force = forceDirection * G * mass / sqrtDist;
    //    Debug.DrawLine(other.position, transform.position, Color.blue);
    //    Debug.DrawRay(other.position, force, Color.red);

    //    other.AddForce(force, ForceMode2D.Force);
    //}

    //void AngleGravityPull(Rigidbody2D other)
    //{
    //    Vector2 velocity = other.velocity;

    //    //Use the mass as a rotation speed
    //    float rotation = mass * Time.fixedDeltaTime;

    //    //Direction of the rotation
    //    Vector2 toOther = other.position - (Vector2)transform.position;
    //    if (Vector3.Cross(velocity, toOther).z > 0)
    //        rotation *= -1;

    //    //Boost the velocity if the rigidbody is going toward the planet
    //    if(Vector2.Dot(velocity, toOther)>0)
    //        velocity += velocity.normalized * velocitySpeedBoost * Time.fixedDeltaTime;

    //    //Rotate the velocity
    //    velocity = Quaternion.Euler(0, 0, rotation) * velocity;

    //    other.velocity = velocity;
    //}

    public void AngleGravityPull(Vector2 otherPosition, ref Vector2 otherVelocity,out float resultintTorque)
    {
        EmulatePlanetGravity(transform.position, mass, otherPosition, ref otherVelocity, out isBoostingSomething, out resultintTorque);
    }

    public static void EmulatePlanetGravity(Vector2 planetPosition, float mass, Vector2 otherPosition, ref Vector2 otherVelocity, out bool isBoostingSomething, out float resultingTorque)
    {
        //Use the mass as a rotation speed
        float rotation = mass * Time.fixedDeltaTime;

        //Direction of the rotation. If the other is going near the center, keep the default rotation to avoid jittering the preview
        Vector2 toOther = otherPosition - planetPosition;
        bool otherCloseFromCenter = Vector2.Dot(toOther.normalized, otherVelocity.normalized) > 0.9f;
        if (!otherCloseFromCenter && Vector3.Cross(otherVelocity, toOther).z > 0)
            rotation *= -1;

        resultingTorque = rotation * RESULTING_TORQUE_MULT;

        //Boost the velocity if the rigidbody is going toward the planet
        if (Vector2.Dot(otherVelocity, toOther) < 0)
        {
            otherVelocity += otherVelocity.normalized * VELOCITY_BOOST * Time.fixedDeltaTime;
            isBoostingSomething = true;
        }
        else
            isBoostingSomething = false;

        //Rotate the velocity
        otherVelocity = Quaternion.Euler(0, 0, rotation) * otherVelocity;
    }

    public bool PositionIsInGravityArea(Vector2 pos)
    {
        return Vector2.Distance(transform.position, pos) < gravityRadius;
    }

    public static List<PlanetGravity> GetAllPlanetsApplyingGravity(Vector2 position)
    {
        List<PlanetGravity> planets = new List<PlanetGravity>();
        foreach (PlanetGravity planet in allPlanets)
        {
            if (planet.PositionIsInGravityArea(position))
                planets.Add(planet);
        }
        return planets;
    }
}
