using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Ant : MonoBehaviour
{
    [Header("Targets")]
    public Transform playerTarget;
    public Transform patrol1;
    public Transform patrol2;

    private bool patrolTarget;
    private Transform currentTarget;

    [Header("Movement")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Behavior")]
    [SerializeField]
    private bool isIdle;
    public bool jumpEnabled = true;

    [Tooltip("Range of blocks that the unit can see")]
    public float visionRange;

    private Path path;
    private int currentWaypoint = 0;
    private bool isGrounded = false;
    private bool reachedEndOfPath = false;

    [Header("GameObjects")]
    public Transform enemyGFX;
    public GameObject ant;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        isIdle = true;
        currentTarget = patrol1;

        //Square range at start to make it easier to calculate later
        visionRange = visionRange * visionRange;

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        //seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void FixedUpdate()
    {
        PathFollow();
    }

    void PathFollow()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (!isIdle)
            {
                Debug.Log("This is running");
                reachedEndOfPath = true;
                return;
            }
            else
            {
                if(currentTarget.position == ant.transform.position)
                {
                    reachedEndOfPath = true;
                    return;
                }
                else
                {
                    reachedEndOfPath = false;
                }
            }
        }
        else
        {
            reachedEndOfPath = false;
        }

        //Used as a check for jumping
        isGrounded = Physics2D.Raycast(transform.position, -Vector3.up, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);

        Vector2 playerPos = new Vector2(currentTarget.position.x, currentTarget.position.y);
        Vector2 antPos = new Vector2(ant.transform.position.x, ant.transform.position.y);

        //Make sure direction is in x only
        Vector2 direction = (playerPos - antPos).normalized;
        direction.y = 0;
        Vector2 force = new Vector2();// = direction * speed * Time.deltaTime;


        //Issues with y movement, so only moving the x position
        if(playerPos.x > antPos.x)
        {
            if (isIdle)
            {
                force.x = 1 * 75 * Time.deltaTime;
            }
            else
            {
                force.x = 1 * speed * Time.deltaTime;
            }
        }
        else
        {
            if (isIdle)
            {
                force.x = -1 * 75 * Time.deltaTime;
            }
            else
            {
                force.x = -1 * speed * Time.deltaTime;
            }
        }



        //Jumping not implemented, there were issues with anty getting stuck and it was not necessary
        /*       
        if(jumpEnabled && isGrounded)
        {
            if(direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(new Vector2(force.x * 2, 0));
            }
        }*/

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-2, 2, 2);
        }
        else if (force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(2, 2, 2);
        }
    }


    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            if (isIdle)
            {
                float distance = Vector2.Distance(rb.position, currentTarget.position);
                if (patrolTarget && distance  <  0.5f)
                {
                    patrolTarget = false;
                }
                else if(distance < 0.5f)
                {
                    patrolTarget = true;
                }
            }
        }
    }

    private void UpdatePath()
    {
        if(seeker.IsDone())
        {
            if (TargetInRange())
            {
                seeker.StartPath(rb.position, playerTarget.position, OnPathComplete);
                currentTarget = playerTarget;
            }
            //Move between the two idle targets
            else
            {
                //If statement to decide which target to go to
                if (patrolTarget)
                {
                    seeker.StartPath(rb.position, patrol1.position, OnPathComplete);
                    currentTarget = patrol1;
                }
                else
                {
                    seeker.StartPath(rb.position, patrol2.position, OnPathComplete);
                    currentTarget = patrol2;
                }
            }
                          
        }            
    }

    private bool TargetInRange()
    {
        float distance = Mathf.Pow((playerTarget.transform.position.x - ant.transform.position.x), 2)
                                + Mathf.Pow((playerTarget.transform.position.y - ant.transform.position.y), 2);

        bool inRange = distance < visionRange;

        if(inRange)
        {
            isIdle = false;
        }
        else
        {
            isIdle = true;
        }

        return inRange;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Frog hit tognue");
        if (collision.transform.CompareTag("Tongue"))
        {
            Destroy(gameObject);
        }
    }
}
