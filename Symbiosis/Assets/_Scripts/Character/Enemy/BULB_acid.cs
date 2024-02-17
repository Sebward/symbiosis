using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BULB_acid : MonoBehaviour
{
    public GameObject bulb_shoot;
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 2)
        {
            //Debug.Log("New");
            Vector3 spawn_pos = transform.position;
            spawn_pos.z = -1;
            Instantiate(bulb_shoot,spawn_pos,Quaternion.identity);
            timer = 0;
        }
    }
}
