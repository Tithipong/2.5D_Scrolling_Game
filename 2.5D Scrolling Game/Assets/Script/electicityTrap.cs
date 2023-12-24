using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class electicityTrap : MonoBehaviour
{
    public float damage;
    public float damageRate;
    public float pushBackForce;

    float nextDamage;
    bool enemyInRange = false;
    GameObject theEnemy;
    enemyHealth theEnemyHealth;

    void Start()
    {
        nextDamage = Time.time;
        theEnemy = GameObject.FindGameObjectWithTag("Enemy");
        theEnemyHealth = theEnemy.GetComponent<enemyHealth>();
    }

    void Update()
    {
        if (enemyInRange)
        {
            Attack();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyInRange = true;
            nextDamage = Time.time; // Reset the nextDamage timer when player enters range
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyInRange = false;
        }
    }

    void Attack()
    {
        if (nextDamage <= Time.time)
        {
            theEnemyHealth.addDamage(damage);
            nextDamage = Time.time + damageRate;
            pushBack(theEnemy.transform);
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
