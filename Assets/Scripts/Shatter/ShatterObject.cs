using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterObject : MonoBehaviour
{
    public GameObject ShatteredObject;
    GameObject Player;
    Rigidbody rb;
    public bool customSound;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        rb = Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Cannon>().InCannon)
            {
                if(!customSound)
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredient/Destruction Ingrédients", transform.position);
                Destroy(gameObject);
                Instantiate(ShatteredObject, transform.position, transform.rotation);
            }
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Cannon>().InCannon)
        {
            if(!customSound)
                FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredient/Destruction Ingredients");
            Destroy(gameObject);
            Instantiate(ShatteredObject, transform.position, transform.rotation);
        }
    }

    void OnDestroy()
    {
        if (Player != null)
        {
            Player.GetComponent<Cannon>().InCannon = false;
            FMODUnity.RuntimeManager.PlayOneShot("event:/TurboPangolin/CanonReload");
            Player.GetComponent<Cannon>().CanCannon = true;
            Player.GetComponent<PangolinAnimation>().EndCannon = true;
            rb.velocity = (3*-Player.transform.forward + new Vector3(0,2,0))*5f;
        }
    }
}
