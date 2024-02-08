using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class frog_jump : MonoBehaviour
{
    public float jump_force = 20.0f;
    [SerializeField] private bool on_ground = false;
    [SerializeField] private bool in_water = false;
    [SerializeField] private float charge_time = 0.0f;
    private Rigidbody2D rb;
    [SerializeField] private bool jump_left;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null ) rb = transform.AddComponent<Rigidbody2D>();
        rb.freezeRotation = true;
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
                    transform.position += Vector3.up * 0.1f;
                    Vector2 jump_impulse = (Vector2.up * 1.7f + Vector2.left).normalized * jump_force * charge_time;
                    rb.velocity = jump_impulse;
                }
                else
                {
                    transform.position += Vector3.up * 0.1f;
                    Vector2 jump_impulse = (Vector2.up * 1.7f + Vector2.right).normalized * jump_force * charge_time;
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
            on_ground = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("midground"))
        {
            on_ground = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Water"))
        {
            Debug.Log("Noya, look at here!");

        }
    }

}
