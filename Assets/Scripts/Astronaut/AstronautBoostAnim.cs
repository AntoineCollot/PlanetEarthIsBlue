using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautBoostAnim : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        anim.SetBool("IsSpeedingUp", PlanetGravity.AnyPlanetBoostingSomething);
    }
}
