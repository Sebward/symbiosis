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
            Instantiate(bulb_shoot,transform.position,Quaternion.identity);
            timer = 0;
        }
    }
}
