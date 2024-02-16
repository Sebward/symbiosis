using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggManager : MonoBehaviour
{
    [SerializeField]
    private int eggTotal = 0;

    public void addEgg(int eggsAdded = 1)
    {
        eggTotal++;
    }

    public void removeEgg(int eggsRemoved = 1)
    {
        eggTotal = eggTotal - eggsRemoved;
        if(eggTotal < 0)
        {
            eggTotal = 0;
        }
    }
}
