using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggManager : MonoBehaviour
{
    [SerializeField]
    public int eggTotal = 0;
    public EggCollectedUI eggCollectedUI;

    private void Start()
    {
        eggCollectedUI = GameObject.FindObjectOfType<EggCollectedUI>();
        if (eggCollectedUI == null) Debug.LogWarning("No Egg Collected UI found in the scene");
    }
    public void addEgg(int eggsAdded = 1)
    {
        eggTotal++;
        eggCollectedUI.UpdateEggCount(eggTotal);
    }

    public void removeEgg(int eggsRemoved = 1)
    {
        eggTotal = eggTotal - eggsRemoved;
        if(eggTotal < 0)
        {
            eggTotal = 0;
        }

        eggCollectedUI.UpdateEggCount(eggTotal);
    }
}
