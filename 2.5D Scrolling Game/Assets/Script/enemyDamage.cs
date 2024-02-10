using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage;
    public float damageRate;
    public float pushBackForce;
    private float nextDamage;
    private bool playerInRange = false;
    private GameObject thePlayer;
    private PlayerHealth thePlayerHealth;

    public void Start()
    {
        nextDamage = Time.time;
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        thePlayerHealth = thePlayer.GetComponent<PlayerHealth>();
    }

    public void Update()
    {
        if (playerInRange)
        {
            Attack();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            nextDamage = Time.time; // Reset the nextDamage timer when player enters range
        }
    }

    public void OnTriggerExit(Collider other)
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
            if (thePlayerHealth != null)
            {
                thePlayerHealth.addDamage(damage);
                nextDamage = Time.time + damageRate;
                pushBack(thePlayer.transform);
            } 
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
