using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    // Ce script est à poser sur un Empty qui est parent de l'ingrédient destructible
    public GameObject Objet;
    public float Cooldown;
    GameObject Spawned;
    Transform Child;
    float SpawnCount;
    void Start()
    {
        Child = transform.GetChild(0);
    }

    void Update()
    {
        if(Child == null)
        {
            SpawnCount += Time.deltaTime;
        }

        if(SpawnCount >= Cooldown)
        {
            SpawnCount = 0f;

            Spawned = Instantiate(Objet, transform.position, transform.rotation);
            Spawned.transform.parent = this.transform;
            Child = Spawned.transform;
        }
    }
}
