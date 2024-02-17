using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onboarding : MonoBehaviour
{
    private float waittime;
    public GameObject UI_To_Destroy;

    void Start()
    {
        waittime = 6.0f;
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spider") || other.gameObject.CompareTag("Frog"))
        {
            Remove();
        }
    }


    private void Remove()
    {
        Destroy(UI_To_Destroy);
    }
}
