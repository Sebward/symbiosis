using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;


public class frog_movement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] private float f_move_speed = 0.2f;
    [SerializeField] private float f_top_speed = 8.0f;
    [SerializeField] private float f_move_damper = 1.0f;

    [Header("Jump")]
    [SerializeField] private float f_jump_speed = 10.0f;
    [SerializeField] private float f_g = 9.8f;

    [Header("Debug")]
    [SerializeField] private float f_horizontal_velocity = 0.0f;
    [SerializeField] private float f_vertical_velocity = 0.0f;
    [SerializeField] private bool b_grounded = false;
    [SerializeField] private BoxCollider2D b_collider;
    //[SerializeField] private bool b_h_hit = false;

    private void Start()
    {
        b_collider = GetComponent<BoxCollider2D>();
        if (b_collider == null) b_collider = transform.AddComponent<BoxCollider2D>();
    }
    void Update()
    {
        //Horizontal
        //inputs
        if( Input.GetKey(KeyCode.A) )
        {
            f_horizontal_velocity -= f_move_speed;
            //Debug.Log(f_horizontal_velocity.ToString());
        }
        else if ( Input.GetKey(KeyCode.D) )
        {
            f_horizontal_velocity += f_move_speed;
            //Debug.Log(f_horizontal_velocity.ToString());
        }
        else
        {
            if (Mathf.Abs(f_horizontal_velocity) > 0.0f)
            {
                f_horizontal_velocity = (f_horizontal_velocity > 0) ? f_horizontal_velocity - f_move_damper : f_horizontal_velocity + f_move_damper;
                if (Mathf.Abs(f_horizontal_velocity) < f_move_damper)
                {
                    f_horizontal_velocity = 0.0f;
                }
            }
        }
        //horizontal speed control
        if (Mathf.Abs(f_horizontal_velocity) > f_top_speed)
        {
            f_horizontal_velocity = (f_horizontal_velocity > 0) ? f_top_speed : -f_top_speed;
        }

        //Vertical
        //input
        if (Input.GetKeyDown(KeyCode.Space) /*&& b_grounded */){
            f_vertical_velocity = f_jump_speed;
        }
        if (!b_grounded)
        {
            f_vertical_velocity -= f_g;
        }
    }
    private void FixedUpdate()
    {
        Vector3 b_center = b_collider.bounds.center;
        Vector3 b_extents = b_collider.bounds.extents;
        //Debug.Log(b_extents);
        //Vector3 lu = new Vector3(b_center.x - b_extents.x, b_center.y + b_extents.y, transform.position.z);
        //Vector3 ld = new Vector3(b_center.x - b_extents.x, b_center.y - b_extents.y, transform.position.z);
        //Vector3 ru = new Vector3(b_center.x + b_extents.x, b_center.y + b_extents.y, transform.position.z);
        //Vector3 rd = new Vector3(b_center.x + b_extents.x, b_center.y - b_extents.y, transform.position.z);

        float margin = 0.95f;
        Vector3 lu = new Vector3(b_center.x - b_extents.x * margin, b_center.y + b_extents.y * margin, transform.position.z);
        Vector3 ld = new Vector3(b_center.x - b_extents.x * margin, b_center.y - b_extents.y * margin, transform.position.z);
        Vector3 ru = new Vector3(b_center.x + b_extents.x * margin, b_center.y + b_extents.y * margin, transform.position.z);
        Vector3 rd = new Vector3(b_center.x + b_extents.x * margin, b_center.y - b_extents.y * margin, transform.position.z);

        RaycastHit2D[] result = new RaycastHit2D[10];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = false;
        float h_raycast_distance = Mathf.Abs(f_horizontal_velocity * Time.deltaTime) > 0 ? Mathf.Abs(f_horizontal_velocity * Time.deltaTime) : 0.1f;
        int hit_h_lu = Physics2D.Raycast(lu, Vector2.left, contactFilter, result, h_raycast_distance);
        int hit_h_ld = Physics2D.Raycast(ld, Vector2.left, contactFilter, result, h_raycast_distance);
        int hit_h_ru = Physics2D.Raycast(ru, Vector2.right, contactFilter, result, h_raycast_distance);
        int hit_h_rd = Physics2D.Raycast(rd, Vector2.right, contactFilter, result, h_raycast_distance);
        int h_l_hit = (hit_h_lu - 1) + (hit_h_ld - 1);
        int h_r_hit = (hit_h_ru - 1) + (hit_h_rd - 1);

        float v_raycast_distance = Mathf.Abs(f_vertical_velocity * Time.deltaTime) > 0 ? Mathf.Abs(f_vertical_velocity * Time.deltaTime) : 0.1f;
        int hit_v_lu = Physics2D.Raycast(lu, Vector2.up, contactFilter, result, 0.1f);
        int hit_v_ld = Physics2D.Raycast(ld, Vector2.down, contactFilter, result, v_raycast_distance);
        int hit_v_ru = Physics2D.Raycast(ru, Vector2.up, contactFilter, result, 0.1f);
        int hit_v_rd = Physics2D.Raycast(rd, Vector2.down, contactFilter, result, v_raycast_distance);
        int v_u_hit = (hit_v_lu - 1) + (hit_v_ru - 1);
        int v_d_hit = (hit_v_ld - 1) + (hit_v_rd - 1);

        // If it hits something...
        if (h_l_hit > 0)
        {
            f_horizontal_velocity = (f_horizontal_velocity > 0) ? f_horizontal_velocity : 0;
        }
        if (h_r_hit > 0)
        {
            f_horizontal_velocity = (f_horizontal_velocity < 0) ? f_horizontal_velocity : 0;
        }
        if (v_u_hit > 0)
        {
            f_vertical_velocity = (f_vertical_velocity > 0) ? -0.8f*f_vertical_velocity : f_vertical_velocity; 
        }

        if (v_d_hit > 0)
        {
            f_vertical_velocity = (f_vertical_velocity > 0) ? f_vertical_velocity : 0;
            b_grounded = true;
        }
        else b_grounded = false;

        transform.position += new Vector3(f_horizontal_velocity * Time.deltaTime, f_vertical_velocity * Time.deltaTime, 0);      
    }
}
