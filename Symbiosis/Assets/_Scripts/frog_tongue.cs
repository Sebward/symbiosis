using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class frog_tongue : MonoBehaviour
{
    private LineRenderer tongueLine;
    private Vector3 targetPosition;
    private bool tongueOut = false;

    public Transform mousePos;

    PolygonCollider2D polygonCollider2D;
    List<Vector2> colliderPoints = new List<Vector2>();

    public Rigidbody2D frog_rb;

    private void Start()
    {
        frog_rb = mousePos.GetComponentInParent<Rigidbody2D>();

        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        tongueLine = GetComponent<LineRenderer>();
        if (tongueLine == null ) tongueLine = transform.AddComponent<LineRenderer>();

        polygonCollider2D = GetComponent<PolygonCollider2D>();
        if (polygonCollider2D == null ) polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
        polygonCollider2D.isTrigger = true;
    }
    void Update()
    {
        transform.position = mousePos.position;

        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;
            tongueOut = true;
            ExtendTongue();
            //frog_rb.velocity = new Vector2(0,0);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            RetractTongue();
            tongueOut = false;
        }

        tongueLine.SetPosition(0, transform.position);
        CreateTongueCollider();

        if (!tongueOut )
        {
            tongueLine.SetPosition(1, tongueLine.GetPosition(0));
        }
    }

    void ExtendTongue()
    {
        //collision detection
        //RaycastHit2D[] result = new RaycastHit2D[10];
        //ContactFilter2D contactFilter = new ContactFilter2D();
        //contactFilter.useTriggers = false;
        //Vector3 raycastDir = (targetPosition - transform.position).normalized;
        //int hit = Physics2D.Raycast(transform.position, targetPosition - transform.position, contactFilter, result, (targetPosition - transform.position).magnitude);
        //if (hit > 1)
        //{
        //    RaycastHit2D raycastHit = Physics2D.Raycast(transform.position + raycastDir * 2, raycastDir);
        //    targetPosition = raycastHit.point;
        //    Debug.Log("hit: " + hit + " hit point: " + raycastHit.point);
        //}

        List<RaycastHit2D> hit_list = new List<RaycastHit2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = false;
        
        Vector3 raycastDir = (targetPosition - transform.position).normalized;

        int hits = Physics2D.Raycast(transform.position, raycastDir, contactFilter, hit_list, Mathf.Infinity);
        Debug.Log("Hit numbers: " + hits );
        Debug.Log("__________________");
        foreach (RaycastHit2D hit in hit_list)
        {
            Debug.Log(hit.collider.gameObject.name);
        }

        tongueLine.positionCount = 2;
        
        tongueLine.SetPosition(0, transform.position);
        Vector3 tongue_target = hit_list[1].point;
        tongue_target.z = transform.position.z;
        tongueLine.SetPosition(1, tongue_target);

        if (hit_list[1].transform.CompareTag("NonStick"))
        Invoke("RetractTongue", 1f);

    }
    private void CreateTongueCollider()
    {
        //if no collision, return
        if ((tongueLine.GetPosition(0) - tongueLine.GetPosition(1)).magnitude < 0.1f)
        {
            polygonCollider2D.enabled = false;
            return;
        }

        polygonCollider2D.enabled = true;
        colliderPoints = CalculateColliderPoints();
        polygonCollider2D.SetPath(0, colliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));    
    }
    private List<Vector2> CalculateColliderPoints()
    {
        //Get All positions on the line renderer
        Vector3[] positions = new Vector3[tongueLine.positionCount];
        tongueLine.GetPositions(positions);

        //Get the Width of the Line
        float width = tongueLine.startWidth;

        //m = (y2 - y1) / (x2 - x1)
        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        //Calculate the Offset from each point to the collision vertex
        Vector3[] offsets = new Vector3[2];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        //Generate the Colliders Vertices
        List<Vector2> colliderPositions = new List<Vector2> {
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1]
        };

        return colliderPositions;
    }
    public void RetractTongue()
    {
        tongueLine.SetPosition(0, mousePos.position);
        tongueLine.SetPosition(1, tongueLine.GetPosition(0));
    }
}

