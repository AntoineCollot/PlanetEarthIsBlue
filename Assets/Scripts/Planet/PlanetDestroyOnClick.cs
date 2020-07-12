using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDestroyOnClick : MonoBehaviour
{
    PlanetGravity planet;
    GravityAreaRendering gravityRendering;

    [SerializeField] ParticleSystem fxPrefab = null;

    public bool CursorIsOverPlanet
    {
        get
        {
            return Vector2.Distance(transform.position, CursorPosition.worldPosition) < planet.radius;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        planet = GetComponent<PlanetGravity>();
        gravityRendering = GetComponentInChildren<GravityAreaRendering>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && CursorIsOverPlanet)
        {
            if(GameMode.AllowDestructionWhenAttracting || !planet.isAttractingSomething)
            {
                SpawnEffect();
                Destroy(gameObject);
            }
            else
            {
                gravityRendering.FlashColor();
            }
        }
    }

    void SpawnEffect()
    {
        ParticleSystem particles = Instantiate(fxPrefab, transform.position, Quaternion.identity, null);

        //Size
        ParticleSystem.ShapeModule shape = particles.shape;
        shape.radius = planet.radius;

        //Materials
        ParticleSystemRenderer renderer = particles.GetComponent<ParticleSystemRenderer>();
        Material planetMat = planet.GetComponent<SpriteRenderer>().material;
        renderer.material = planetMat;
        renderer.trailMaterial = planetMat;

        Destroy(particles.gameObject, 1);
    }
}
