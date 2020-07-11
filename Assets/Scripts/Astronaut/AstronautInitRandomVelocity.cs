using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautInitRandomVelocity : MonoBehaviour
{
    [Header("Translation")]
    [SerializeField] float minForce = 1;
    [SerializeField] float maxForce = 2;

    [Header("Rotation")]
    [SerializeField] float maxTorque = 2;

    // Start is called before the first frame update
    void Start()
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2);
        Vector2 randomDir = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));

        float randomForce = Random.Range(minForce, maxForce);

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(randomDir * randomForce, ForceMode2D.Impulse);
        //rigidbody.AddTorque(Random.Range(-maxTorque, maxTorque));
        rigidbody.AddTorque(maxTorque);
    }
}
