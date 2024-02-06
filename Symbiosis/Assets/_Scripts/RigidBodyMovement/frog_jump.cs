using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog_jump : MonoBehaviour
{
    public float jump_force = 10.0f;
    [SerializeField] private bool on_ground = false;
    [SerializeField] private float charge_time = 0.0f;
    private Rigidbody2D rb;
    [SerializeField] private bool jump_left;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2.0f;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            charge_time += Time.deltaTime;
            jump_left = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            charge_time += Time.deltaTime;
            jump_left = false;
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (!on_ground) charge_time = 0;
            else
            {
                Debug.Log("JUMP: " + charge_time);
                if (jump_left)
                {
                    Vector2 jump_impulse = (Vector2.up + Vector2.left).normalized * jump_force * charge_time;
                    rb.velocity = jump_impulse;
                }
                else
                {
                    Vector2 jump_impulse = (Vector2.up + Vector2.right).normalized * jump_force * charge_time;
                    rb.velocity = jump_impulse;
                }

                charge_time = 0.0f;
            }
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("midground"))
        {
            Debug.Log("Hit");
            on_ground = true;
        }
    }
}
