using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAnimationHandler : MonoBehaviour
{
    Animator a;                     //pass through animation triggers

    private int idleHash;
    //private int groundHash;
    private int jumpHash;           // hashes for various movement transitions (purposefully overbuilt for extensibility)
    private int midairHash;
    private int landingHash;
    //private int landingInWaterHash;
    //private int jumpFromWaterHash;
    //private int landToWaterHash;
    //private int waterToLandHash;
    //private int onSpiderHash;
    //private int offSpiderHash;
    //private int ontoWallHash;
    //private int offWallHash;
    private int waterHash;

    void Awake()
    {
        a = GetComponent<Animator>();

        idleHash = Animator.StringToHash("idle");
        //groundHash = Animator.StringToHash("onGround");
        jumpHash = Animator.StringToHash("jump");
        midairHash = Animator.StringToHash("midair");
        landingHash = Animator.StringToHash("landing");
        //landingInWaterHash = Animator.StringToHash("landing in water");
        //jumpFromWaterHash = Animator.StringToHash("jump from water");
        //landToWaterHash = Animator.StringToHash("land to water");
        //waterToLandHash = Animator.StringToHash("water to land");
        //onSpiderHash = Animator.StringToHash("on spider");
        //offSpiderHash = Animator.StringToHash("off spider");
        //ontoWallHash = Animator.StringToHash("onto wall");
        //offWallHash = Animator.StringToHash("off wall");
        waterHash = Animator.StringToHash("water");
    }

    public void playFrogIdleAnim()                  { a.SetTrigger(idleHash); }
    public void setFrogGround()                     { a.SetBool(midairHash, false); /*Debug.Log("Frog has landed.");*/  }
    public void playFrogJumpAnim()                  { a.SetTrigger(jumpHash); a.SetBool(midairHash, true); /*Debug.Log("Frog has jumped.");*/ }
    //public void playFrogMidairAnim()              { a.SetTrigger(midairHash); }
    public void setFrogMidair()                     { a.SetBool(midairHash, true); }
    public void playFrogLandingAnim()               { a.SetTrigger(landingHash); }
    //public void playFrogLandingInWaterAnim()    { a.SetTrigger(landingInWaterHash); }
    //public void playFrogJumpFromWaterAnim()     { a.SetTrigger(jumpFromWaterHash); }
    //public void playFrogLandToWaterAnim()       { a.SetTrigger(landToWaterHash); }
    //public void playFrogWaterToLandAnim()       { a.SetTrigger(waterToLandHash); }
    //public void playFrogOnSpiderAnim()          { a.SetTrigger(onSpiderHash); }
    //public void playFrogOffSpiderAnim()         { a.SetTrigger(offSpiderHash); }
    //public void playFrogOnWallAnim()            { a.SetTrigger(ontoWallHash); }
    //public void playFrogOffWallAnim()           { a.SetTrigger(offWallHash); }
    public void setInWater()                        { a.SetBool(waterHash, true); }
    public void setOutWater()                       { a.SetBool(waterHash, false); }
    public void debugLogger() { Debug.Log("Animation Played"); }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
