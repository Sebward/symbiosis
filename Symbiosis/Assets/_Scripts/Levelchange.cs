using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Levelchange : MonoBehaviour
{
    //public int level;

    public GameObject go_target_location;
    private Vector2 v_player_target_location;

    public GameObject camera_target_location;
    private Vector2 v_camera_target_location;

    public GameObject Camera;
    //public GameObject Camera2;
    //public GameObject Camera3;
    //public GameObject Camera4;
    //----------------------------------]
    //----------------------------------]

    void Start()
    {
        v_player_target_location = go_target_location.transform.position;
        v_camera_target_location = camera_target_location.transform.position;
        //Camera.SetActive(true);
        //Camera2.SetActive(false);
        //Camera3.SetActive(false);
        //Camera4.SetActive(false);
    }
    //--------------------------------------------------------------]
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Player touched loading zone");
        if (other.gameObject.CompareTag("Player"))
        {
            //if (gameObject.tag == ("Forward")) //what in God's name is this ;_; ~QP
            //{
            //    if (level == 1)
            //    {
            //        GoTo(v_player_target_location, Camera1,Camera2);
            //    }
            //    else if (level == 2)
            //    {
            //        GoTo(v_player_target_location, Camera2, Camera3);
            //    }
            //    else if (level == 3)
            //    {
            //        GoTo(v_player_target_location, Camera3, Camera4);
            //    }
            //}

            GoTo(other.gameObject, v_player_target_location, v_camera_target_location);
        }
    }
    //--------------------------------------------------------------]
    public void GoTo(GameObject Player, Vector2 PlayerTargetLoc, Vector2 CameraTargetLoc)
    {
        Player.transform.position = new Vector3(PlayerTargetLoc.x, PlayerTargetLoc.y, Player.transform.position.z);
        Camera.transform.position = new Vector3(CameraTargetLoc.x, CameraTargetLoc.y, Camera.transform.position.z);

        //New_Camera.SetActive(true);
        //Old_Camera.SetActive(false);
        //level++;
        Debug.Log("Camera Position: " + Camera.transform.position);
    }
    //public void SetStart()
    //{
    //    if( level == 1)
    //    {
    //        v_player_target_location = go_target_location.transform.position;
    //    }
    //}
}
