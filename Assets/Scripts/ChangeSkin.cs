using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public List<GameObject> ToDestroy;
    public List<GameObject> ToActivate;
    Animator animator;

    public Avatar avatar2;
    public RuntimeAnimatorController Controller2;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && GetComponent<Cannon>().CanCannon)
        {
            animator.runtimeAnimatorController = Controller2;
            animator.avatar = avatar2;
            foreach(var item in ToActivate)
            {
                item.SetActive(true);
            }
            foreach(var item in ToDestroy)
            {
                Destroy(item);
            }
            
            GetComponent<ChangeSkin>().enabled = false;
        }
    }
}
