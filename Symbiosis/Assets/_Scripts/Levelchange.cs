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

    void Start()
    {
        v_player_target_location = go_target_location.transform.position;
        v_camera_target_location = camera_target_location.transform.position;
    }
    //--------------------------------------------------------------]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spider") || other.gameObject.CompareTag("Frog"))
        {
            GoTo(other.gameObject, v_player_target_location, v_camera_target_location);
        }
    }
    //--------------------------------------------------------------]
    public void GoTo(GameObject Player, Vector2 PlayerTargetLoc, Vector2 CameraTargetLoc)
    {
        Player.transform.position = new Vector3(PlayerTargetLoc.x, PlayerTargetLoc.y, Player.transform.position.z);
        Camera.transform.position = new Vector3(CameraTargetLoc.x, CameraTargetLoc.y, Camera.transform.position.z);
        Debug.Log("Camera Position: " + Camera.transform.position);
    }
}
