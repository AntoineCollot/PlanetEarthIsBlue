using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStarColorToPalette : MonoBehaviour
{
    public int colorId =0;
    [Range(0,1)] public float alpha = 1;

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem particles = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mainModule = particles.main;
        Color c = PlanetColorManager.GetColor(colorId);
        c.a = alpha;
        mainModule.startColor = c;
        particles.Play();
    }
}
