using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelchange : MonoBehaviour
{
    public GameObject go_Level_2_Start;
    public GameObject Player;

    public Vector2 v_Level_2_Start;

    void Start()
    {
         v_Level_2_Start = go_Level_2_Start.transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.transform.position = v_Level_2_Start;
        }
    }
}
