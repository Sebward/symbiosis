using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BULB_acid : MonoBehaviour
{
    BoxCollider2D trigger;
    ParticleSystem party;

    private void Awake()
    { 
        trigger = GetComponent<BoxCollider2D>();
        party = GetComponent<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        var stop = party.emission;
        stop.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision entered -- thing should activate");
        var start = party.emission;
        if (collision.transform.CompareTag("Spider") || collision.transform.CompareTag("Frog"))
        {
            start.enabled = true;
        }
    }
}
