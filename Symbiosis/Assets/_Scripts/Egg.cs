using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public EggManager eggManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Frog") || collision.transform.CompareTag("Spider"))
        {
            eggManager.addEgg();
            Destroy(gameObject);
        }
    }
}
