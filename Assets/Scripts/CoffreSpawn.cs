using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffreSpawn : MonoBehaviour
{
    public GameObject[] Gemmes;
    int GemmeChoose;
    float doIt;
    float LifeTime;
    
    void Start()
    {

    }

    
    void Update()
    {
        LifeTime += Time.deltaTime;
        if(LifeTime <= 20f)
        {
            doIt += Time.deltaTime;
            if(doIt >= 0.05f)
            {
                GemmeChoose = Random.Range(0,Gemmes.Length);
                GameObject Pop = Instantiate(Gemmes[GemmeChoose],transform.position, Quaternion.identity);
                Pop.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2,2), 7f, Random.Range(-2,2)), ForceMode.Impulse);
                doIt = 0;
            }
        }
    }
}
