using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class spider_crawl : MonoBehaviour
{
    public float move_speed = 5.0f;
    public float max_climb_height = 3.0f;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsule_collider;
    [SerializeField] private bool can_move = false;
    [SerializeField] private bool can_crawl = false;
    [SerializeField] private float h, v;

    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    public bool dead = false;

    public Transform change_image_detect;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null ) spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2.0f;
        rb.freezeRotation = true;

        capsule_collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            can_move = false;
            can_crawl = false;
            rb.isKinematic = true;
        }
        if (can_move || can_crawl)
        {
            h = move_speed * Input.GetAxis("Horizontal");
            if (h < 0) spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;
            v = move_speed * Input.GetAxis("Vertical");
            Vector2 move_vector = Vector2.right * h + Vector2.up * v;
            rb.velocity = move_vector;
        }
        
    }
    private void FixedUpdate()
    {
        if (can_move && !can_crawl)
        {
            RaycastHit2D[] results = new RaycastHit2D[10];
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.useTriggers = false;
            float cast_distance = capsule_collider.bounds.extents.y + max_climb_height;
            int hit_ground = capsule_collider.Cast(Vector2.down, results, cast_distance, true);
            if (hit_ground < 1)
            {
                Debug.Log("Too high");
                if (v > 0) { v = 0; }
                Vector2 move_vector = Vector2.right * h + Vector2.up * v;
                rb.velocity = move_vector;
            }
        }

        List<RaycastHit2D> hit_list = new List<RaycastHit2D>();
        ContactFilter2D c_contactFilter = new ContactFilter2D();
        c_contactFilter.useTriggers = true;

        int hits = Physics2D.CircleCast(change_image_detect.position, 0.1f, Vector2.zero, c_contactFilter, hit_list);
        Debug.Log("Hit numbers: " + hits + " list size: " + hit_list.Count);
        Debug.Log("__________________");
        
        bool crawl = false;
        foreach (RaycastHit2D hit in hit_list)
        {
            Debug.Log(hit.collider.gameObject.name);
            if(hit.transform.CompareTag("SpiderCrawl"))
            {
                spriteRenderer.sprite = sprites[1];
                crawl = true;
            }
        }

        if (crawl == false) spriteRenderer.sprite = sprites[0];
        Debug.Log("spider crawling ? " + crawl);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("midground"))
        {
            can_move = true;
            //Debug.Log("Spider Move");
        }
        if (collision.transform.CompareTag("Frog"))
        {
            can_move = true;
        }
        if (collision.transform.CompareTag("BULB"))
        {
            if (!dead)
            {
                Debug.Log("Spider die!!!!");
                dead = true;
                can_crawl = false;
                rb.gravityScale = 0.0f;
                rb.velocity = new Vector2(0, 0);
                transform.position -= new Vector3(0, 0.5f, 0);
                rb.transform.localScale = new Vector3(2, -2, 2);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("midground") )
        {
            can_move = false;
            //Debug.Log("Spider Don't Move");
        }
        if (collision.transform.CompareTag("Frog"))
        {
            can_move = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Water"))
        {
            Debug.Log("Spider die!!!!");
            dead = true;
            can_crawl = false;
            rb.gravityScale = 0.0f;
            transform.position -= new Vector3(0, 1, 0);
            rb.transform.localScale = new Vector3(2, -2, 2);
            
            //rb.velocity = Vector2.zero;     
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("SpiderCrawl"))
        {
            //Debug.Log("Spider can crawl");
            can_crawl = true;
            rb.gravityScale = 0;
            
        }
        if (collision.transform.CompareTag("Tongue"))
        {
            can_crawl = true;
            rb.gravityScale = 0;
        }
        if (collision.transform.CompareTag("Water"))
        {
            Debug.Log("Spider dead!!!!");
            can_crawl = false;
            rb.gravityScale = 0.0f;
            rb.velocity = Vector2.zero;     
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("SpiderCrawl"))
        {
            can_crawl = false;
            rb.gravityScale = 2;
            
        }
        if (collision.transform.CompareTag("Tongue"))
        {
            can_crawl = false;
            rb.gravityScale = 2;
        }
    }
}
