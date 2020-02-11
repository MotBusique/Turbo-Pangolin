using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCollid : MonoBehaviour
{
    public GameObject[] Gemmes;
    public Transform spawnPoint;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<Cannon>().InCannon)
            {
                GameObject GO = Instantiate(Gemmes[Random.Range(0,Gemmes.Length)], spawnPoint.position, Quaternion.identity);
                GO.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0,7), Random.Range(-7,7), Random.Range(0,7)), ForceMode.Impulse);
                GameObject GO2 = Instantiate(Gemmes[Random.Range(0,Gemmes.Length)], spawnPoint.position, Quaternion.identity);
                GO2.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0,7), Random.Range(-7,7), Random.Range(0,7)), ForceMode.Impulse);
                GameObject GO3 = Instantiate(Gemmes[Random.Range(0,Gemmes.Length)], spawnPoint.position, Quaternion.identity);
                GO3.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0,7), Random.Range(-7,7), Random.Range(0,7)), ForceMode.Impulse);
                GameObject GO4 = Instantiate(Gemmes[Random.Range(0,Gemmes.Length)], spawnPoint.position, Quaternion.identity);
                GO4.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0,7), Random.Range(-7,7), Random.Range(0,7)), ForceMode.Impulse);
                GameObject GO5 = Instantiate(Gemmes[Random.Range(0,Gemmes.Length)], spawnPoint.position, Quaternion.identity);
                GO5.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0,7), Random.Range(-7,7), Random.Range(0,7)), ForceMode.Impulse);
            }
        }
    }
}
