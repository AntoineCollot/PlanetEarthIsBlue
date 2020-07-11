using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStarColorToPalette : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem particles = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mainModule = particles.main;
        mainModule.startColor = PlanetColorPalettes.MainColor;
        particles.Play();
    }
}
