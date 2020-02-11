using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    Rigidbody rb;
    TrailRenderer Trail;
    ParticleSystem SmokeParticles;
    [HideInInspector] public bool InTube;
    public float power;
    public bool InCannon = false;
    public bool CanCannon = true;
    [HideInInspector] public float CannonTime;
    public float CannonCoolDown;
    bool ClicGauche;
    float Reload;
    bool FixedPass;

    void Start()
    {
        //Son Ambiance
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/Son Ambiance");

        rb = GetComponent<Rigidbody>();
        Trail = GetComponent<TrailRenderer>();
        SmokeParticles = GetComponent<ParticleSystem>();
    }


    void Update()
    {
        if(FixedPass)
            ClicGauche = Input.GetMouseButtonDown(0);
        FixedPass = false;
        
        if (InCannon)
        {
            if(!InTube)
                CannonTime += Time.deltaTime;

            Trail.enabled = true;
            GetComponent<PlayerControl>().OnGround = false;

            if(InTube)
            {
                CannonTime = 0;
                rb.velocity *= 1.03f;
            }

            if (CannonTime >= CannonCoolDown)
            {
                CannonTime=0;
                InCannon = false;
                GetComponent<PangolinAnimation>().EndCannon = true;
            }

            gameObject.GetComponent<Rigidbody>().useGravity = false;
            this.transform.GetChild(3).GetComponent<Collider>().material.bounciness = 1;

            this.transform.GetChild(3).GetComponent<CapsuleCollider>().enabled = false;
            this.transform.GetChild(3).GetComponent<SphereCollider>().enabled = true;
            
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            this.transform.GetChild(3).GetComponent<Collider>().material.bounciness = 0;
            Trail.enabled = false;
            CannonTime = 0;

            this.transform.GetChild(3).GetComponent<CapsuleCollider>().enabled = true;
            this.transform.GetChild(3).GetComponent<SphereCollider>().enabled = false;
        }

        if(!CanCannon)
        {
            
            if(Reload >= 4)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/TurboPangolin/CanonReload");
                Reload = 0;
                CanCannon = true;
            }
        }
        if (!CanCannon && !InCannon)
        {
            SmokeParticles.Play();
            Reload += Time.deltaTime;
        }
        
        if(CanCannon || InCannon)
        {
            SmokeParticles.Pause();
            SmokeParticles.Clear();
        }
        
    }

    void FixedUpdate()
    {
        FixedPass = true;
        if (ClicGauche && CanCannon)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/TurboPangolin/Canon Pangolin");
            CanCannon = false;
            InCannon = true;
            CannonTime = 0;
            GetComponent<PlayerControl>().jumped = true;
            transform.rotation = transform.GetChild(0).transform.rotation;
            rb.velocity = transform.forward * power;
        }
    }
}