using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider_climb : MonoBehaviour
{
    //public float jump_force = 10.0f;
    //[SerializeField] private bool on_ground = false;
    //[SerializeField] private float charge_time = 0.0f;
    private Rigidbody2D rb;
    public float speedX, speedY;
    private BoxCollider2D b_collider;
    Vector3 b_extents;
    int hit_up, hit_down, hit_left, hit_right;
    //[SerializeField] private bool jump_left;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        b_collider = rb.GetComponent<BoxCollider2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        speedX = 0;
        speedY = 0;
        //Vector3 b_center = b_collider.bounds.center;
        b_extents = b_collider.bounds.extents;
    }
    private void FixedUpdate()
    {
        RaycastHit2D[] result = new RaycastHit2D[10];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = false;

        hit_up = Physics2D.Raycast(b_collider.bounds.center, Vector2.up, contactFilter, result, 1.1f * b_extents.y);
        hit_down = Physics2D.Raycast(b_collider.bounds.center, Vector2.down, contactFilter, result, 1.1f * b_extents.y);
        hit_left = Physics2D.Raycast(b_collider.bounds.center, Vector2.left, contactFilter, result, 1.1f * b_extents.x);
        hit_right = Physics2D.Raycast(b_collider.bounds.center, Vector2.right, contactFilter, result, 1.1f * b_extents.x);
        Debug.Log(hit_up + ", " + hit_down + ", " + hit_left + ", " + hit_right);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if ((hit_left > 0 || hit_right > 0) && hit_up <= 0)
            {
                speedY = 15.0f;
            }
            else
            {
                speedY = 0;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if ((hit_left > 0 || hit_right > 0) && hit_down <= 0)
            {
                speedY = -15.0f;
            }
            else
            {
                speedY = 0;
            }
        }
        else
        {
            speedY = 0;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if ((hit_up > 0 || hit_down > 0) && hit_left <= 0)
            {
                speedX = -15.0f;
            }
            else
            {
                speedX = 0;
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if ((hit_up > 0 || hit_down > 0) && hit_right <= 0)
            {
                speedX = 15.0f;
            }
            else
            {
                speedX = 0;
            }
        }
        else
        {
            speedX = 0;
        }

        rb.velocity = new Vector2(speedX, speedY);
        //transform.position += new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime, 0);
        Debug.Log(rb.velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("midground"))
        {
            Debug.Log("Hit");
            
        }
    }
}

