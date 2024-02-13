using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntAnimationHandler : MonoBehaviour
{
    //Animator which stores/handles animation params
    Animator a;

    //param hashes
    private int walkingHash;
    
    void Awake()
    {
        a = GetComponent<Animator>();
        walkingHash = Animator.StringToHash("walking");
    }

    public void setAntWalking(){ a.SetBool(walkingHash, true); }
    public void setAntIdle() { a.SetBool(walkingHash, false); }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
