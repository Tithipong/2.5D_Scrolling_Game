using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class enemyDamage : MonoBehaviour
{
    public float damage;
    public float damageRate;
    public float pushBackForce;

    float nextDamage;
    bool playerInRage = false;
    GameObject thePlayer;
    PlayerHealth thePlayerHealth;
    // Start is called before the first frame update
    void Start()
    {
        nextDamage = Time.time;
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        thePlayerHealth = thePlayer.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRage) Attack();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRage = true;
        }
    }
    void OntrigerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRage = false;
        }
    }
    void Attack()
    {
        if (nextDamage <= Time.time)
        {
            thePlayerHealth.addDamage(damage);
            nextDamage = Time.time + damageRate;

            pushBack(thePlayer.transform);
        }
    }
    void pushBack(Transform pushObject)
    {
        Vector3 pushDirrection = new Vector3(0, (pushObject.position.y - transform.position.y), 0).normalized;
        pushDirrection *= pushBackForce;

        Rigidbody pushedRB = pushObject.GetComponent<Rigidbody>();
        pushedRB.velocity = Vector3.zero;
        pushedRB.AddForce(pushDirrection, ForceMode.Impulse);
    }
}
