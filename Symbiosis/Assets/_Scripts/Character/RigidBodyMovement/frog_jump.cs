using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class frog_jump : MonoBehaviour
{
    public Slider jumpPowerSlider;
    public float jump_force = 20.0f;
    [SerializeField] private bool on_ground = false;
    [SerializeField] private bool in_water = false;
    [SerializeField] private float charge_time = 0.0f;
    private Rigidbody2D rb;
    [SerializeField] private bool jump_left;
    //Prefab components for handling visuals
    protected FrogAnimationHandler anime;
    public bool finished_jump = false;
    protected SpriteRenderer sprite;

    //Box Collider for state checks and rescaling
    BoxCollider2D box;

    //Getting hit variables
    private float invincibleTimer;
    private bool invincible;

    //values for collider rescale
    Vector2 offset1R = new Vector2(0.095f, -0.42f);
    Vector2 offset1L = new Vector2(-0.095f, -0.42f);
    Vector2 size1 = new Vector2(0.95f, 0.44f);

    Vector2 offset2R = new Vector2(0.105f, -0.41f);
    Vector2 offset2L = new Vector2(-0.105f, -0.41f);
    Vector2 size2 = new Vector2(0.97f, 0.46f);

    Vector2 offset3R = new Vector2(0.090f, -0.35f);
    Vector2 offset3L = new Vector2(-0.090f, -0.35f);
    Vector2 size3 = new Vector2(1.04f, 0.58f);

    Vector2 offset4R = new Vector2(0.0040f, -0.185f);
    Vector2 offset4L = new Vector2(-0.0040f, -0.185f);
    Vector2 size4 = new Vector2(1.18f, 0.91f);

    private void Start()
    {
        invincible = false;
        rb = GetComponent<Rigidbody2D>();
        if (rb == null ) rb = transform.AddComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.gravityScale = 2.0f;
        anime = GetComponent<FrogAnimationHandler>();
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            charge_time += Time.deltaTime;
            jump_left = true;
            jumpPowerSlider.value += .1f;
            //Fixed. ~QP
            //transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            charge_time += Time.deltaTime;
            jump_left = false;
            jumpPowerSlider.value +=.1f;
            //Fixed. ~QP
            //transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            jumpPowerSlider.value -= .3f;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (on_ground || in_water)
            {
                anime.playFrogJumpAnim();
                //Debug.Log("Midair");
                if (in_water) charge_time *= 0.5f;
                //Debug.Log("JUMP: " + charge_time);
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
                //jumpPowerSlider.value = 0.0f;
                anime.playFrogJumpAnim();
                //Debug.Log("Midair");
            }
            else
            {
                charge_time = 0;
            }
        }
        //else anime.playFrogIdleAnim();

        if (!on_ground && rb.velocity.y < 0)
        {
            finished_jump = true;
            //anime.playFrogLandingAnim();
            //anime.setFrogGround();
            //Debug.Log("Landing");
        }

        if(in_water)
        {
            rb.gravityScale = 0.8f;
            
        }
        else
        {
            rb.gravityScale = 2.0f;
        }
        //flip the sprite about the x axis
        sprite.flipX = jump_left;


        //Debug.Log("Invincible Timer:" + invincibleTimer);

        if (invincible)
        {
            if (invincibleTimer < 0)
            {
                //Debug.Log("No longer invincible");
                invincible = false;
                GetComponent<Renderer>().material.color = Color.white;
                return;
            }
            invincibleTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("midground") || collision.transform.CompareTag("Spider"))
        {
            if(finished_jump)
            {
                anime.playFrogLandingAnim();
                anime.setFrogGround();
                //Debug.Log("Landing");
                finished_jump = false;
                jumpPowerSlider.value = 0;
            }
            //anime.playFrogLandingAnim();
            //anime.setFrogGround();
            //Debug.Log("Landing");
            anime.playFrogIdleAnim();
            //Debug.Log("Landed on ground");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("midground") || collision.transform.CompareTag("Spider"))
        {
            on_ground = true;
            //anime.playFrogLandingAnim();
            //anime.setFrogGround();
            //Debug.Log("Landed on ground");
        }
        if (collision.transform.CompareTag("Wasp"))
        {
            if (!invincible)
            {
                //Debug.Log("Hit by Wasp");
                invincible = true;
                invincibleTimer = 5;

                //Drop eggs?
                GetComponent<Renderer>().material.color = Color.red;
                MyCoroutine();
                Reload();
            }
        }
        if (collision.transform.CompareTag("BULB"))
        {

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("midground"))
        {
            on_ground = false;
            //Debug.Log("in air");
            //anime.playFrogJumpAnim();
            //Debug.Log("Midair");
            //anime.setFrogMidair();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Water"))
        {
            //Debug.Log("in");
            in_water = true;
            //anime.setInWater();
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Water"))
        {
            //Debug.Log("out");
            in_water = false;
            //anime.setOutWater();
        }
    }

    public void sprite1()
    {
        box.offset = (jump_left ? offset1L : offset1R);
        box.size = size1;
    }
    public void sprite2()
    {
        box.offset = (jump_left ? offset2L : offset2R);
        box.size = size2;
    }
    public void sprite3()
    {
        box.offset = (jump_left ? offset3L : offset3R);
        box.size = size3;
    }
    public void sprite4()
    {
        box.offset = (jump_left ? offset4L : offset4R);
        box.size = size4;
    }
    public void Reload()
    {
        SceneManager.UnloadSceneAsync(1);

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    IEnumerator MyCoroutine()
    {
        //Debug.Log("Restart Countdown Started");

        yield return new WaitForSeconds(3);

        //Debug.Log("Restarting");
    }
}
