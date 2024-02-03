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
    [SerializeField] private bool b_jump = false;
    [SerializeField] private BoxCollider2D b_collider;

    private void Start()
    {
        b_collider = GetComponent<BoxCollider2D>();
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
            b_jump = true;
        }

    }
    private void FixedUpdate()
    {
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

        //vertical speed control
        if (!b_jump)
        {
            if (!b_grounded)
            {
                f_vertical_velocity -= f_g * Time.deltaTime;
            }
            else
            {
                f_vertical_velocity = 0.0f;
            }
        }
        else b_jump = false;

        transform.position += new Vector3(f_horizontal_velocity * Time.deltaTime, f_vertical_velocity * Time.deltaTime, 0);

        Debug.DrawRay(transform.position, -Vector2.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("midground"))
        {
            //int hit_count = Raycast(transform.position, Vector2.down);
            b_grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("midground"))
        {
            b_grounded = false;
        }
    }
}
