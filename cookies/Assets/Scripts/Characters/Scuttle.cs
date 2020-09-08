using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scuttle : MonoBehaviour
{
    public Transform roachGoal;
    public float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));

        if (transform.position.x <= roachGoal.position.x)
            Destroy(gameObject);
    }
}
