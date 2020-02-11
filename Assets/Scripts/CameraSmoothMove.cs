using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothMove : MonoBehaviour
{
    GameObject Pos2;
    GameObject Cam2;
    GameObject Cam1;
    bool backing;
    bool going;
    float timer;

    void Start()
    {
        Cam2 = GameObject.Find("Camera2");
        Pos2 = GameObject.Find("CamPos2");
        Cam1 = GameObject.Find("Camera");
    }


    void Update()
    {
        if(Input.GetMouseButton(1) && GetComponent<Cannon>().CanCannon)
        {
            Cam2.GetComponent<Camera>().enabled = true;
            Cam1.GetComponent<Camera>().enabled = false;
            if(Cam2.transform.position.x > Pos2.transform.position.x-0.5f && Cam2.transform.position.x < Pos2.transform.position.x+0.5f && Cam2.transform.position.y > Pos2.transform.position.y-0.5f && Cam2.transform.position.y < Pos2.transform.position.y+0.5f && Cam2.transform.position.z > Pos2.transform.position.z-0.5f && Cam2.transform.position.z < Pos2.transform.position.z+0.5f)
            {
                Cam2.transform.position = Pos2.transform.position;
                Cam2.transform.rotation = Pos2.transform.rotation;
            }
            else
            {
                Cam2.transform.position = Vector3.Lerp(Cam2.transform.position, Pos2.transform.position, 0.4f);
                Cam2.transform.rotation = Quaternion.Lerp(Cam2.transform.rotation, Pos2.transform.rotation, 0.4f);
            }
        }

        if(!Input.GetMouseButton(1) || !GetComponent<Cannon>().CanCannon)
        {
            if(Cam2.transform.position.x > Cam1.transform.position.x-0.5f && Cam2.transform.position.x < Cam1.transform.position.x+0.5f && Cam2.transform.position.y > Cam1.transform.position.y-0.5f && Cam2.transform.position.y < Cam1.transform.position.y+0.5f && Cam2.transform.position.z > Cam1.transform.position.z-0.5f && Cam2.transform.position.z < Cam1.transform.position.z+0.5f)
            {
                Cam1.GetComponent<Camera>().enabled = true;
                Cam2.GetComponent<Camera>().enabled = false;
                Cam2.transform.position = Cam1.transform.position;
                Cam2.transform.rotation = Cam1.transform.rotation;
            }
            else
            {
                Cam2.transform.position = Vector3.Lerp(Cam2.transform.position, Cam1.transform.position, 0.6f);
                Cam2.transform.rotation = Quaternion.Lerp(Cam2.transform.rotation, Cam1.transform.rotation, 0.6f);
            }
        }
    }
}
