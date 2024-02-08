using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAnimationHandler : MonoBehaviour
{
    Animator a;                     //pass through animation triggers

    private int idleHash;
    private int jumpHash;           // hashes for various movement transitions (purposefully overbuilt for extensibility)
    private int midairHash;
    private int landingHash;
    private int landingInWaterHash;
    private int jumpFromWaterHash;  //I anticipate only these first four will be used, though I'm planning for an eventuality
    private int landToWaterHash;    //in which we're able to make unique animations for each of these.
    private int waterToLandHash;
    private int onSpiderHash;       //might also need these, not sure though
    private int offSpiderHash;
    private int ontoWallHash;
    private int offWallHash;

    void Awake()
    {
        a = GetComponent<Animator>();

        idleHash = Animator.StringToHash("idle");
        jumpHash = Animator.StringToHash("jump");
        midairHash = Animator.StringToHash("midair");
        landingHash = Animator.StringToHash("landing");
        landingInWaterHash = Animator.StringToHash("landing in water");
        jumpFromWaterHash = Animator.StringToHash("jump from water");
        landToWaterHash = Animator.StringToHash("land to water");
        waterToLandHash = Animator.StringToHash("water to land");
        onSpiderHash = Animator.StringToHash("on spider");
        offSpiderHash = Animator.StringToHash("off spider");
        ontoWallHash = Animator.StringToHash("onto wall");
        offWallHash = Animator.StringToHash("off wall");
    }

    public void playFrogIdleAnim()              { a.SetTrigger(idleHash); }
    public void playFrogJumpAnim()              { a.SetTrigger(jumpHash); }
    public void playFrogMidairAnim()            { a.SetTrigger(midairHash); }
    public void playFrogLandingAnim()           { a.SetTrigger(landingHash); }
    public void playFrogLandingInWaterAnim()    { a.SetTrigger(landingInWaterHash); }
    public void playFrogJumpFromWaterAnim()     { a.SetTrigger(jumpFromWaterHash); }
    public void playFrogLandToWaterAnim()       { a.SetTrigger(landToWaterHash); }
    public void playFrogWaterToLandAnim()       { a.SetTrigger(waterToLandHash); }
    public void playFrogOnSpiderAnim()          { a.SetTrigger(onSpiderHash); }
    public void playFrogOffSpiderAnim()         { a.SetTrigger(offSpiderHash); }
    public void playFrogOnWallAnim()            { a.SetTrigger(ontoWallHash); }
    public void playFrogOffWallAnim()           { a.SetTrigger(offWallHash); }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
