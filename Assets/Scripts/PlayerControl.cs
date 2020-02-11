using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    Transform Came;
    Transform Cible;
    Transform BCible;
    Transform RCible;
    Transform LCible;
    float RotateY;
    float RotateX;
    public float maxSpeed;
    public float speed;
    public bool OnGround;
    bool SpaceBar;
    bool ForwardInput;
    bool BackInput;
    bool RightInput;
    bool LeftInput;
    bool FixedPass;
    bool HasJumped;
    [HideInInspector] public bool jumped;
    float step = 20f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Came = transform.GetChild(0);
        Cible = transform.GetChild(0).GetChild(1);
        BCible = transform.GetChild(0).GetChild(2);
        RCible = transform.GetChild(0).GetChild(3);
        LCible = transform.GetChild(0).GetChild(4);

    }

    void Update()
    {

        RotateY += Input.GetAxis("Mouse X");
        Came.transform.rotation = Quaternion.Euler(0f, RotateY/2, 0);

        RotateX += Input.GetAxis("Mouse Y");
        RotateX = Mathf.Clamp(RotateX, -80f, 80f);
        Came.transform.rotation = Quaternion.Euler(-RotateX, RotateY, 0);
        

        if (Input.GetKeyDown("r"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            GetComponent<PangolinAnimation>().volePango.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            GetComponent<PangolinAnimation>().marchePango.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
            
        if(FixedPass)
        {
            SpaceBar = Input.GetKeyDown(KeyCode.Space);
            ForwardInput = Input.GetKey("z");
            BackInput = Input.GetKey("s");
            RightInput = Input.GetKey("d");
            LeftInput = Input.GetKey("q");
        }

        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(GetComponent<Animator>().GetLayerIndex("Base Layer")).IsName("Air"))
        {
            GetComponent<BoxCollider>().size = new Vector3(0.57f, 5f, 0.63f);
            Debug.Log("lolé");
        }
        else
        {
            GetComponent<BoxCollider>().size = new Vector3(0.57f, 0.23f, 0.63f);
        }


        FixedPass = false;
    }

    void FixedUpdate()
    {
        FixedPass= true;

        if(OnGround)
        {
            if (SpaceBar && !HasJumped)
            {
                GetComponent<Animator>().SetTrigger("Jump");
                Invoke("Jump", 0.3f);
                HasJumped = true;
            }


            if(ForwardInput)
            {
                Vector3 newDir = Vector3.RotateTowards(transform.forward, new Vector3(Cible.transform.position.x - transform.position.x, 0f, Cible.transform.position.z - transform.position.z).normalized , 3f* Time.deltaTime, step);
                transform.rotation = Quaternion.LookRotation(newDir);
                if(rb.velocity.magnitude < maxSpeed)
                    rb.velocity += transform.forward*speed*Time.timeScale;
            }


            if (BackInput)
            {
                Vector3 newDir = Vector3.RotateTowards(transform.forward, new Vector3(BCible.transform.position.x - transform.position.x, 0f, BCible.transform.position.z - transform.position.z).normalized , 3f* Time.deltaTime, step);
                transform.rotation = Quaternion.LookRotation(newDir);
                if(rb.velocity.magnitude < maxSpeed)
                    rb.velocity += transform.forward*speed*Time.timeScale;
            }

            if(RightInput)
            {
                Vector3 newDir = Vector3.RotateTowards(transform.forward, new Vector3(RCible.transform.position.x - transform.position.x, 0f, RCible.transform.position.z - transform.position.z).normalized , 3f* Time.deltaTime, step);
                transform.rotation = Quaternion.LookRotation(newDir);
                if(rb.velocity.magnitude < maxSpeed)
                    rb.velocity += transform.forward*speed*Time.timeScale;
            }

            if(LeftInput)
            {
                Vector3 newDir = Vector3.RotateTowards(transform.forward, new Vector3(LCible.transform.position.x - transform.position.x, 0f, LCible.transform.position.z - transform.position.z).normalized , 3f* Time.deltaTime, step);
                transform.rotation = Quaternion.LookRotation(newDir);
                if(rb.velocity.magnitude < maxSpeed)
                    rb.velocity += transform.forward*speed*Time.timeScale;
            }
        }

        if(!OnGround)
        {
            if(ForwardInput)
            {
                if(rb.velocity.magnitude < maxSpeed)
                    rb.velocity += transform.forward*speed*Time.timeScale;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, (Cible.transform.position - transform.position).normalized , 2f* Time.deltaTime, 1f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }


            if (BackInput)
            {
                if(rb.velocity.magnitude < maxSpeed)
                    rb.velocity += transform.forward*speed*Time.timeScale;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, (BCible.transform.position - transform.position).normalized , 2f* Time.deltaTime, 1f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sol" && jumped && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(GetComponent<Animator>().GetLayerIndex("Base Layer")).IsName("Air"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/TurboPangolin/Pango Sol");
            jumped = false;
            
        }
        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(GetComponent<Animator>().GetLayerIndex("Base Layer")).IsName("Air"))
                GetComponent<Animator>().SetTrigger("Atterissage");

        
        OnGround = true;
    }

    void OnTriggerStay(Collider other)
    {
        OnGround = true;
        if (!gameObject.GetComponent<Cannon>().InCannon)
            gameObject.GetComponent<Cannon>().CanCannon = true;

        rb.drag = 1f;

        if(jumped)
            Invoke("Land", 0.3f);
    }

    void OnTriggerExit(Collider other)
    {

        OnGround = false;
        rb.drag = 0.5f;
    }

    void Jump()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/TurboPangolin/Jump Pangolin");
        rb.velocity += transform.up * 10f;
        jumped = true;
        HasJumped = false;
    }

    void Land()
    {
        GetComponent<Animator>().SetTrigger("Atterissage");
    }
}
