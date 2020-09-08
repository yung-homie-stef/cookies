using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zigzag : MonoBehaviour
{

    Vector3 zigzaggerPos = new Vector3();
    float time = 0.0f;

    [Range(0.0f, 1.0f)]
    public float amplitude;
    public int speed = 0;

    // Update is called once per frame
    void Update()
    {
        zigzaggerPos.x = amplitude * Mathf.Cos(time);
        zigzaggerPos.z += speed * Time.deltaTime;

        time += Time.deltaTime;

        transform.position = zigzaggerPos;
    }
}
