using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Levelchange : MonoBehaviour
{
    public int level;
    public GameObject go_target_location;
    public Vector2 v_target_location;
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Camera3;
    public GameObject Camera4;
    
    public GameObject Player;
    //----------------------------------]
    //----------------------------------]

    void Start()
    {
        v_target_location = go_target_location.transform.position;
        Camera1.SetActive(true);
        Camera2.SetActive(false);
        Camera3.SetActive(false);
        Camera4.SetActive(false);
    }
    //--------------------------------------------------------------]
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Frog touched");
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.tag == ("Forward"))
            {
                if (level == 1)
                {
                    GoTo(v_target_location, Camera1,Camera2);
                }
                else if (level == 2)
                {
                    GoTo(v_target_location, Camera2, Camera3);
                }
                else if (level == 3)
                {
                    GoTo(v_target_location, Camera3, Camera4);
                }
            }
        }
    }
    //--------------------------------------------------------------]
    public void GoTo(Vector2 Target,GameObject Old_Camera, GameObject New_Camera )
    {
        Player.transform.position = Target;
        New_Camera.SetActive(true);
        Old_Camera.SetActive(false);
        level++;
    }
    public void SetStart()
    {
        if( level == 1)
        {
            v_target_location = go_target_location.transform.position;
        }
    }
}
