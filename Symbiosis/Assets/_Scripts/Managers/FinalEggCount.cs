using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalEggCount : MonoBehaviour
{
    public TMP_Text finalCount;
    public EggManager eggManager;

    private void OnEnable()
    {
        finalCount = GetComponent<TMP_Text>();
        eggManager = FindObjectOfType<EggManager>();
        ShowText();
    }
    public void ShowText()
    {
        finalCount.text = "You collected " + eggManager.eggTotal + " eggs!";
    }
}
