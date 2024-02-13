using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Wasp : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    [Tooltip("Range of blocks that the unit can see")]
    public float visionRange;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    public Transform enemyGFX;
    public GameObject wasp;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        //Square range at start to make it easier to calculate later
        visionRange = visionRange * visionRange;

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        //seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void Update()
    {
        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(1, 1, 1);
        }
        else if (force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(-1, 1, 1);
        }
    }


    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void UpdatePath()
    {
        if(seeker.IsDone())
        {
            float distance = Mathf.Pow((target.transform.position.x - wasp.transform.position.x), 2)
                                + Mathf.Pow((target.transform.position.y - wasp.transform.position.y), 2);

            //Debug.Log("Distance " + distance + " Range: " + visionRange);
            //Debug.Log(target.transform.position.x - wasp.transform.position.x);

            if (distance < visionRange)
                seeker.StartPath(rb.position, target.position, OnPathComplete);
        }            
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Spider"))
        {
            Destroy(gameObject);
        }
    }
}
