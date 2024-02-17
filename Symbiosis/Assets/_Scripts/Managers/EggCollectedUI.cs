using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EggCollectedUI : MonoBehaviour
{
    public TMP_Text eggCountText;
    private void Start()
    {
        eggCountText = GetComponent<TMP_Text>();
    }
    public void UpdateEggCount(int eggs)
    {
        eggCountText.text = "Eggs Collected: " + eggs;
    }
}
