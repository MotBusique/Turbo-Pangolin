using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeBoost : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.parent.CompareTag("Player"))
        {
            other.gameObject.transform.parent.GetComponent<Cannon>().InTube = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent.CompareTag("Player"))
        {
            other.gameObject.transform.parent.GetComponent<Cannon>().InTube = false;
        }
    }
}
