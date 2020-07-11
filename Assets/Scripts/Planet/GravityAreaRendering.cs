using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAreaRendering : MonoBehaviour
{
    [Range(0, 1)] public float displayRadius = 1;
    [Range(0,1)] public float displayFade = 0.01f;

    [Header("Colors")]
    Color idleColor;
    [SerializeField] Color clickedColor = Color.red;
    [SerializeField] float clickedColorAnimTime = 0.3f;
    [SerializeField] AnimationCurve clickAnimCurve = null;
    PlanetGravity planet;
    Material instancedMaterial;
    Animator anim;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        planet = GetComponentInParent<PlanetGravity>();
        anim = GetComponentInParent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        idleColor = sprite.color;
        instancedMaterial = sprite.material;
        UpdateAreaSize();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        instancedMaterial.SetFloat("_CircleRadius", displayRadius);
        instancedMaterial.SetFloat("_CircleFade", displayFade);
        anim.SetBool("IsVisible", planet.CursorIsInGravityArea);

        UpdateAreaSize();

        if (Input.GetMouseButtonDown(0) && planet.CursorIsInGravityArea)
        {
            StopAllCoroutines();
            StartCoroutine(UpdateSpriteColor());
        }
    }

    void UpdateAreaSize()
    {
        transform.localScale = Vector3.one * planet.gravityRadius * 2 / transform.parent.localScale.x;
    }

    IEnumerator UpdateSpriteColor()
    {
        float t = 0;
        while(t<1)
        {
            t += Time.deltaTime / clickedColorAnimTime;

            sprite.color = Color.Lerp(idleColor, clickedColor, clickAnimCurve.Evaluate(t));

            yield return null;
        }

        sprite.color = idleColor;
    }
}
