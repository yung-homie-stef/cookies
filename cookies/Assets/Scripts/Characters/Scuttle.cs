using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scuttle : MonoBehaviour
{
    public Transform roachGoal;
    public float speed = 0.5f;

    float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Lerp(gameObject.transform.position.x, roachGoal.position.x, time), 0, gameObject.transform.position.z);

        time += speed * Time.deltaTime;

        if (transform.position.x == roachGoal.position.x)
            Destroy(gameObject);
    }
}
