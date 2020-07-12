using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautInitVelocity : MonoBehaviour
{
    [Header("Translation")]
    [SerializeField] float force = 1;
    [SerializeField] float angle = 0;

    [Header("Rotation")]
    [SerializeField] float torque = 2;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(dir * force, ForceMode2D.Impulse);
        //rigidbody.AddTorque(Random.Range(-maxTorque, maxTorque));
        rigidbody.AddTorque(torque);
    }
}
