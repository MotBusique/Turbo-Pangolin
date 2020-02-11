using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeBack : MonoBehaviour
{
    public GameObject Objet;
    public float Cooldown;
    GameObject Spawned;
    Transform Child;
    float SpawnCount;
    // Start is called before the first frame update
    void Start()
    {
        Child = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount <1)
        {
            SpawnCount += Time.deltaTime;
        }

        if(SpawnCount >= Cooldown)
        {
            SpawnCount = 0f;

            Spawned = Instantiate(Objet, transform.position, transform.rotation);
            Spawned.transform.parent = this.transform;
            Child = transform.GetChild(0);
        }
    }
}
