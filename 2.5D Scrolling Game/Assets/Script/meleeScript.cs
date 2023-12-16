using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeScript : MonoBehaviour
{
    public float damage;
    public float knockBack;
    public float knockBackRadius;
    public float meleeRate;

    float nextMelee;
    int shootabkeMask;

    Animator myAnim;
    playerController myPC;


    // Start is called before the first frame update
    void Start()
    {
        shootabkeMask = LayerMask.GetMask("Shootable");
        myAnim = transform.root.GetComponent<Animator>();
        myPC = transform.root.GetComponent<playerController>();
        nextMelee = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float melee = Input.GetAxis("Fire2");

        if(melee > 0 && nextMelee < Time.time && !(myPC.getRunning())){
            myAnim.SetTrigger("gunMelee");
            nextMelee = Time.time + meleeRate;

            //do damage
            Collider[] atacked = Physics.OverlapSphere(transform.position, knockBack,shootabkeMask);        
        }
    }
}
