using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class spider_crawl : MonoBehaviour
{
    public float move_speed = 5.0f;
    public float max_climb_height = 3.0f;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsule_collider;
    [SerializeField] private bool can_move = false;
    [SerializeField] private bool can_crawl = false;
    [SerializeField] private float h, v;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2.0f;
        rb.freezeRotation = true;

        capsule_collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (can_move || can_crawl)
        {
            h = move_speed * Input.GetAxis("Horizontal");
            v = move_speed * Input.GetAxis("Vertical");
            Vector2 move_vector = Vector2.right * h + Vector2.up * v;
            rb.velocity = move_vector;
        }

    }
    private void FixedUpdate()
    {
        if (can_move && !can_crawl)
        {
            RaycastHit2D[] results = new RaycastHit2D[10];
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.useTriggers = false;
            float cast_distance = capsule_collider.bounds.extents.y + max_climb_height;
            int hit_ground = capsule_collider.Cast(Vector2.down, results, cast_distance, true);
            if (hit_ground < 1)
            {
                Debug.Log("Too high");
                if (v > 0) { v = 0; }
                Vector2 move_vector = Vector2.right * h + Vector2.up * v;
                rb.velocity = move_vector;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("midground"))
        {
            can_move = true;
            //Debug.Log("Spider Move");
        }
        if (collision.transform.CompareTag("Frog"))
        {
            can_move = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("midground") )
        {
            can_move = false;
            //Debug.Log("Spider Don't Move");
        }
        if (collision.transform.CompareTag("Frog"))
        {
            can_move = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Water"))
        {
            Debug.Log("Spider die!!!!");
            can_crawl = false;
            rb.gravityScale = 0.0f;
            transform.position -= new Vector3(0, 1, 0);
            rb.transform.localScale = new Vector3(2, -2, 2);
            //rb.velocity = Vector2.zero;     
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("SpiderCrawl"))
        {
            //Debug.Log("Spider can crawl");
            can_crawl = true;
            rb.gravityScale = 0;
        }
        if (collision.transform.CompareTag("Tongue"))
        {
            can_crawl = true;
        }
        if (collision.transform.CompareTag("Water"))
        {
            Debug.Log("Spider dead!!!!");
            can_crawl = false;
            rb.gravityScale = 0.0f;
            rb.velocity = Vector2.zero;     
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("SpiderCrawl"))
        {
            can_crawl = false;
            rb.gravityScale = 2;
        }
        if (collision.transform.CompareTag("Tongue"))
        {
            can_crawl = false;
            rb.gravityScale = 2;
        }
    }
}
