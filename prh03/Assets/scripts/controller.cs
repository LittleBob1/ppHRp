using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveVec;
    private bool inAir;

    public ParticleSystem pWater;

    public float speed;
    public float speedJump;

    public float kX;
    public float kY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveVec = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        rb.velocity = moveVec;

        if (Input.GetKey(KeyCode.Space) && !inAir)
        {
            inAir = true;
            rb.velocity = new Vector2(rb.velocity.x, speedJump);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "spring")
        {
            waterPhys wat = collision.gameObject.GetComponent<waterPhys>();
            if (Mathf.RoundToInt(rb.velocity.y) > 0)
            {
                wat.Splash(-rb.velocity.y / kY);
                pWater.Play();
            }
            else if (Mathf.RoundToInt(rb.velocity.y) < 0)
            {
                wat.Splash(rb.velocity.y / kY);
                pWater.Play();
            }
            else if (Mathf.Abs(rb.velocity.x) > 0)
            {
                wat.Splash(Mathf.Abs(rb.velocity.x / kX));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            inAir = false;
    }
}
