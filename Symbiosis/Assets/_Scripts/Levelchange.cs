using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Levelchange : MonoBehaviour
{
    public GameObject go_target_location;
    private Vector2 v_player_target_location;

    public GameObject camera_target_location;
    private Vector2 v_camera_target_location;

    public GameObject Camera;

    private GameObject[] players = new GameObject[2];

    void Start()
    {
        v_player_target_location = go_target_location.transform.position;
        v_camera_target_location = camera_target_location.transform.position;

        players[0] = GameObject.FindWithTag("Frog");
        players[1] = GameObject.FindWithTag("Spider");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Player touched loading zone");
        if (other.gameObject.CompareTag("Spider") || other.gameObject.CompareTag("Frog")) GoTo(v_player_target_location, v_camera_target_location);
    }
    public void GoTo(Vector2 PlayerTargetLoc, Vector2 CameraTargetLoc)
    {
        //Player.transform.position = new Vector3(PlayerTargetLoc.x, PlayerTargetLoc.y, Player.transform.position.z);
        foreach(var player in players)
        {
            player.transform.position = new Vector3(PlayerTargetLoc.x, PlayerTargetLoc.y, player.transform.position.z);
        }

        Camera.transform.position = new Vector3(CameraTargetLoc.x, CameraTargetLoc.y, Camera.transform.position.z);

        //Debug.Log("Camera Position: " + Camera.transform.position);
    }
}
