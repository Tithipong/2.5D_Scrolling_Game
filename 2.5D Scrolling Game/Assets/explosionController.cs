using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class explosionController : MonoBehaviour
{
    public Light explposionLight;
    public float power;
    public float radius;
    public float damage;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 explosionPos= transform.position;
        Collider[]  colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders){
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0f, ForceMode.Impulse);
            }
            if (hit.tag == "Player")
            {
                PlayerHealth thePlayerHealth = hit.gameObject.GetComponent<PlayerHealth>();
                thePlayerHealth.addDamage(damage);

            }else if(hit.tag == "Enemy"){
                enemyHealth theEnemyHealth = hit.gameObject.GetComponent<enemyHealth>();
                theEnemyHealth.addDamage(damage);

            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        explposionLight.intensity = Mathf.Lerp(explposionLight.intensity, 0f,5*Time.time);
    }
}
