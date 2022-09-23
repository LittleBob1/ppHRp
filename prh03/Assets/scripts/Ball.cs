using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "spring")
        {
            WaterPhys wat = collision.gameObject.GetComponent<WaterPhys>();
            if (Mathf.RoundToInt(rb.velocity.y) < 0)
            {
                wat.Splash(-rb.velocity.y / 100);
            }
        }
    }
}
