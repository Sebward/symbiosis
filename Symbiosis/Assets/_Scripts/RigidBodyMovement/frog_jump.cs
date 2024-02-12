using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class frog_jump : MonoBehaviour
{
    public float jump_force = 20.0f;
    [SerializeField] private bool on_ground = false;
    [SerializeField] private float charge_time = 0.0f;
    private Rigidbody2D rb;
    [SerializeField] private bool jump_left;


    //Getting hit variables
    private float invincibleTimer;
    private bool invincible;

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
                    Vector2 jump_impulse = (Vector2.up * 1.7f + Vector2.left).normalized * jump_force * charge_time;
                    rb.velocity = jump_impulse;
                }
                else
                {
                    Vector2 jump_impulse = (Vector2.up * 1.7f + Vector2.right).normalized * jump_force * charge_time;
                    rb.velocity = jump_impulse;
                }

                charge_time = 0.0f;
            }
        }

        Debug.Log("Invincible Timer:" + invincibleTimer);

        if (invincible)
        {
            if(invincibleTimer < 0)
            {
                Debug.Log("No longer invincible");
                invincible = false;
                GetComponent<Renderer>().material.color = Color.white;
                return;              
            }
            invincibleTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("midground"))
        {
            Debug.Log("Hit");
            on_ground = true;
        }
        if (collision.transform.CompareTag("Wasp"))
        {
            if (!invincible)
            {
                Debug.Log("Hit by Wasp");
                invincible = true;
                invincibleTimer = 5;

                //Drop eggs?
                GetComponent<Renderer>().material.color = Color.red;
            }        
        }
    }
}
