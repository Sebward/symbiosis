using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class frog_movement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] private float f_move_speed = 1.0f;
    [SerializeField] private float f_top_speed = 2.0f;
    [SerializeField] private float f_move_damper = 0.5f;

    [Header("Jump")]
    [SerializeField] private float f_jump_speed = 15.0f;
    [SerializeField] private float f_g = 9.8f;

    [Header("Debug")]
    [SerializeField] private float f_horizontal_velocity = 0.0f;
    [SerializeField] private float f_vertical_velocity = 0.0f;


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

        if ( Input.GetKeyDown(KeyCode.Space))
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
            if (Mathf.Abs(f_horizontal_velocity) < 0.02f)
            {
                f_horizontal_velocity = 0.0f;
            }
        }


        //vertical speed control
        f_vertical_velocity -= f_g;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        if (hit.collider != null)
        {
            Debug.Log("hit");
            f_vertical_velocity = 0.0f;
        }

        transform.position += new Vector3(f_horizontal_velocity * Time.deltaTime, f_vertical_velocity * Time.deltaTime, 0);

    }
    
}
