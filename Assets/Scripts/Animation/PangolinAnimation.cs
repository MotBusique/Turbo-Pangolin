using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PangolinAnimation : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    public bool IsWalking;
    bool tamponWalking;
    bool tamponAir;
    bool playWalk;
    float IdleTimer;
    public bool EndCannon;
    
    [HideInInspector] public FMOD.Studio.EventInstance marchePango;
    [HideInInspector] public FMOD.Studio.EventInstance volePango;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        marchePango = FMODUnity.RuntimeManager.CreateInstance("event:/TurboPangolin/Footsteps Pango");
        volePango = FMODUnity.RuntimeManager.CreateInstance("event:/TurboPangolin/Air Pango");
    }

    // Update is called once per frame
    
    void Update()
    {
        marchePango.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        volePango.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        if (!IsWalking)
            IdleTimer += Time.deltaTime;
        
        if(GetComponent<PlayerControl>().OnGround)
            IsWalking = rb.velocity.magnitude >= 1f || rb.velocity.magnitude <= -1f;
        else
            IsWalking = false;

        if (tamponWalking != IsWalking)
        {   
            tamponWalking = IsWalking;
            if (tamponWalking == true)
                playWalk = true;
        }

        Animating();
        PlaySound();
    }

    void Animating()
    {
        anim.SetBool("Walking", IsWalking);
        anim.SetBool("InCannon", GetComponent<Cannon>().InCannon);

        if(EndCannon)
        {
            EndCannon = false;
            anim.SetTrigger("EndCannon");
        }
    }

    void PlaySound()
    {
        /////////////////////
        //Marche
        if (playWalk)
        {
            marchePango.start();
            playWalk = false;
        }
         if (!IsWalking)
         {
            marchePango.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
         }
        /////////////////////
        //Vol
        if(!GetComponent<PlayerControl>().OnGround)
        {
            if(rb.velocity.magnitude >= 1f || rb.velocity.magnitude<= -1f)
            {
                if(tamponAir)
                {
                    volePango.start();
                    tamponAir = false;
                }
            }
            volePango.setParameterByName("VolumeAirPango", rb.velocity.magnitude);
        }
        if (GetComponent<PlayerControl>().OnGround)
        {
            volePango.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            tamponAir = true;
        }
        if(rb.velocity.magnitude < 1f && rb.velocity.magnitude > -1f)
        {
            volePango.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            tamponAir = true;
        }
        /////////////////////

        
    }
}
