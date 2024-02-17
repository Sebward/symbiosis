using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Sprites;

public class Levelchange : MonoBehaviour
{
    public GameObject go_target_location;
    private Vector2 v_player_target_location;
    public GameObject Camera;
    public GameObject camera_target_location;
    public GameObject EndScreen;
    public int Currentlevel;


    public int LevelChangeWaitTime = 3;
    public Sprite None;
    public Sprite One;
    public Sprite Both;
    public SpriteRenderer spriterender;
    public int Players_At_End = 0;

    private Vector2 v_camera_target_location;
    private GameObject[] players = new GameObject[2];

    void Start()
    {
        v_player_target_location = go_target_location.transform.position;
        v_camera_target_location = camera_target_location.transform.position;

        players[0] = GameObject.FindWithTag("Frog");
        players[1] = GameObject.FindWithTag("Spider");

        EndScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spider") || other.gameObject.CompareTag("Frog"))
        {
            Players_At_End++;
            Debug.Log("Players at end increased too:" + Players_At_End);
            if (Players_At_End == 1)
            {
                spriterender.sprite = One;
            }
            if (Players_At_End == 2)
            {
                spriterender.sprite = Both;
                Invoke("WaitSeconds",LevelChangeWaitTime);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spider") || other.gameObject.CompareTag("Frog"))
        {
            Players_At_End--;
            spriterender.sprite = None;

        }
    }
    public void GoTo(Vector2 PlayerTargetLoc, Vector2 CameraTargetLoc)
    {
        //Player.transform.position = new Vector3(PlayerTargetLoc.x, PlayerTargetLoc.y, Player.transform.position.z);
        foreach(var player in players)
        {
            player.transform.position = new Vector3(PlayerTargetLoc.x, PlayerTargetLoc.y, player.transform.position.z);
        }

        Camera.transform.position = new Vector3(CameraTargetLoc.x, CameraTargetLoc.y, Camera.transform.position.z);

        if (Currentlevel < 4)
        {
            Currentlevel++;
        }
        else
        {
            EnableEndScreen();
        }

        //Debug.Log("Camera Position: " + Camera.transform.position);
    }
    void WaitSeconds()
    {
        Debug.Log("Wait Started for: 5");
        Debug.Log("Wait over!");
        GoTo(v_player_target_location, v_camera_target_location);
    }

    public void EnableEndScreen()
    {
        EndScreen.SetActive(true);
    }
}
