using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickUpController : MonoBehaviour
{
    public float healthAmount;
    public AudioClip healthPickupSound;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().addHealth(healthAmount);
            Destroy(transform.root.gameObject);
            AudioSource.PlayClipAtPoint(healthPickupSound,transform.position,1f);
        }
    }
}
