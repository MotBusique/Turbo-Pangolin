using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScope : MonoBehaviour
{
    bool ClicDroit;
    LineRenderer ScopeLine;
    Camera Cam1;
    Camera Cam2;
    Transform Came;
    float FixedDT;
    bool scoping;
    public GameObject ScopeEnd;
    GameObject Blurer;
    GameObject LastScope;
    FMOD.Studio.EventInstance visePango;


    // Start is called before the first frame update
    void Start()
    {
        ScopeLine = GetComponent<LineRenderer>();
        Cam1 = GameObject.Find("Camera").GetComponent<Camera>();
        Cam2 = GameObject.Find("Camera2").GetComponent<Camera>();
        FixedDT = Time.fixedDeltaTime;
        Blurer = GameObject.Find("Blurer");
        Came = transform.GetChild(0);
        //son Vise
        visePango = FMODUnity.RuntimeManager.CreateInstance("event:/TurboPangolin/SlowMoVisé");
    }

    // Update is called once per frame
    void Update()
    {
        if (LastScope != null)
            Destroy(LastScope);

        ClicDroit = Input.GetMouseButton(1);
        Time.fixedDeltaTime = FixedDT * Time.timeScale;

        if(Input.GetMouseButtonDown(1) && GetComponent<Cannon>().CanCannon)
        {
            visePango.start();
            scoping = true;
        }

        if(ClicDroit && GetComponent<Cannon>().CanCannon)
        {
            transform.rotation = Came.transform.rotation;
            Blurer.GetComponent<MeshRenderer>().enabled = true;
            ScopeLine.enabled = true;
            ScopeLine.SetPosition (0, transform.position + transform.up/1.6f);
            Time.timeScale = 0.4f;

            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit))
            {
                ScopeLine.SetPosition (1,hit.point);
                LastScope = Instantiate(ScopeEnd, hit.point, Quaternion.identity);
            }
            else
                ScopeLine.SetPosition(1, transform.position + transform.forward*100);
        }
        else
        {
            
            Blurer.GetComponent<MeshRenderer>().enabled = false;
            ScopeLine.enabled = false;
            Time.timeScale = 1f;
        }
        if (Input.GetMouseButtonUp(1) && scoping)
        {
            visePango.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            FMODUnity.RuntimeManager.PlayOneShot("event:/TurboPangolin/SlowMoVisé 2");
            scoping = false;
        }
        if(Input.GetMouseButton(0) && !GetComponent<Cannon>().CanCannon && scoping)
        {
            visePango.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            FMODUnity.RuntimeManager.PlayOneShot("event:/TurboPangolin/SlowMoVisé 2");
            scoping = false;
        }
    }
}
