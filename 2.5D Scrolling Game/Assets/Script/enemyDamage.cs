using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage;
    public float damageRate;
    public float pushBackForce;

    float nextDamage;
    bool playerInRange = false;
    GameObject thePlayer;
    PlayerHealth thePlayerHealth;

    void Start()
    {
        nextDamage = Time.time;
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        thePlayerHealth = thePlayer.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (playerInRange)
        {
            Attack();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            nextDamage = Time.time; // Reset the nextDamage timer when player enters range
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
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
        Vector3 pushDirection = new Vector3(0, (pushObject.position.y - transform.position.y), 0).normalized;
        pushDirection *= pushBackForce;

        Rigidbody pushedRB = pushObject.GetComponent<Rigidbody>();
        pushedRB.velocity = Vector3.zero;
        pushedRB.AddForce(pushDirection, ForceMode.Impulse);
    }
}
