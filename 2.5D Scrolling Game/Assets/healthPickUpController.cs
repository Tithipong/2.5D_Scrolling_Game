using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickUpController : MonoBehaviour
{
    public float healthAmount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().addHealth(healthAmount);
            Destroy(transform.root.gameObject);
        }
    }
}
