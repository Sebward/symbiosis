<<<<<<< HEAD
=======
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
>>>>>>> parent of d7623e5 (Merge branch 'main' of https://github.com/Sebward/symbiosis)
using Unity.VisualScripting;
using UnityEngine;

public class frog_movement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] private float f_move_speed = 0.2f;
    [SerializeField] private float f_top_speed = 8.0f;
    [SerializeField] private float f_move_damper = 0.1f;

    [Header("Jump")]
    [SerializeField] private float f_jump_speed = 10.0f;
    [SerializeField] private float f_g = 9.8f;

    [Header("Debug")]
    [SerializeField] private float f_horizontal_velocity = 0.0f;
    [SerializeField] private float f_vertical_velocity = 0.0f;
    [SerializeField] private bool b_grounded = false;
<<<<<<< HEAD
    [SerializeField] private bool b_jump = false;
    [SerializeField] private BoxCollider2D b_collider;
=======

>>>>>>> parent of d7623e5 (Merge branch 'main' of https://github.com/Sebward/symbiosis)
    private void Start()
    {
        Rigidbody2D rb = transform.AddComponent<Rigidbody2D>();
        //rb.isKinematic = true;
    }
    void Update()
    {
        //inputs
        if( Input.GetKey(KeyCode.A) )
        {
            f_horizontal_velocity -= f_move_speed;
        }

        if ( Input.GetKey(KeyCode.D) )
        {
            f_horizontal_velocity += f_move_speed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            f_vertical_velocity += f_jump_speed;
        }

        //horizontal speed control
        if (Mathf.Abs(f_horizontal_velocity) > f_top_speed)
        {
            f_horizontal_velocity = (f_horizontal_velocity > 0) ? f_top_speed : -f_top_speed;
        }

        if (Mathf.Abs(f_horizontal_velocity) > 0.0f)
        {
            f_horizontal_velocity = (f_horizontal_velocity > 0) ? f_horizontal_velocity - f_move_damper : f_horizontal_velocity + f_move_damper;
            if (Mathf.Abs(f_horizontal_velocity) < f_move_damper * 0.1f)
            {
                f_horizontal_velocity = 0.0f;
            }
        }

        float ray_distance = b_collider.size.y / 2.0f;
        RaycastHit2D[] hits = new RaycastHit2D[1];
        int col_count = b_collider.Raycast(Vector2.down, hits, ray_distance);
        if (col_count > 0)  b_grounded = true;
        else b_grounded = false;

        //vertical speed control
        if (!b_grounded)
        {
            f_vertical_velocity -= f_g * Time.deltaTime;
        }
        

        transform.position += new Vector3(f_horizontal_velocity * Time.deltaTime, f_vertical_velocity * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< HEAD
        float ray_distance = b_collider.size.y / 2.0f + 0.01f;
        RaycastHit2D[] hits = new RaycastHit2D[1];
        
        int col_count = b_collider.Raycast(Vector2.down, hits, ray_distance);
        if (col_count > 0)
        {
            b_grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        float ray_distance = b_collider.size.y / 2.0f;
        RaycastHit2D[] hits = new RaycastHit2D[1];

        int col_count = b_collider.Raycast(Vector2.down, hits, ray_distance);
        if (col_count > 0)
        {
            b_grounded = false;
        }
=======
        b_grounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        b_grounded = false;
>>>>>>> parent of d7623e5 (Merge branch 'main' of https://github.com/Sebward/symbiosis)
    }
}
