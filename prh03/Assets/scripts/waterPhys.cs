using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPhys : MonoBehaviour
{

    private float acceleration;
    private float height = 0;
    private float targetHeight = 0;

    public float velocity;
    [HideInInspector]
    public float targHeig;
    [HideInInspector]
    public float k;
    [HideInInspector]
    public float d;

    void Update()
    {
        height = transform.position.y;

        float x = height - targetHeight;

        float loss = -d * velocity;

        acceleration = -k * x + loss;

        velocity += acceleration;

        transform.position = new Vector3(transform.position.x, transform.position.y + velocity, 0);
    }

    public void Initialized()
    {
        height = transform.position.y;
        targetHeight = transform.position.y;
        velocity = 0;
    }

    public void Splash(float velocityX)
    {
        this.velocity = velocityX;
    }

}
