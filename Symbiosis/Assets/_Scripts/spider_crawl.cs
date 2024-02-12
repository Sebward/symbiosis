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
        if (can_move)
        {
            float h = move_speed * Input.GetAxis("Horizontal");
            float v = move_speed * Input.GetAxis("Vertical");
            Vector2 move_vector = Vector2.right * h + Vector2.up * v;
            rb.velocity = move_vector;
        }

    }
    private void FixedUpdate()
    {
        if (can_move)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("midground"))
        {
            can_move = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("midground"))
        {
            can_move = false;
        }
    }
}