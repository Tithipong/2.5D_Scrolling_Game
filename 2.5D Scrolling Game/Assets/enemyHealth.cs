using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{
    public float enemyMaxHealth;
    public float damageModifier;
    public GameObject damageParticles;
    public bool drops;
    public GameObject drop;
    public AudioClip deathSound;
    public bool canBurm;
    public float burnDamage;
    public float burmTime;
    public GameObject burnEffect;

    bool onFire;
    float nextBurn;
    float burnInterval = 1f;
    float endBurn;

    float currentHealth;

    public Slider enemyHealthIndicator;
    

    AudioSource enemyAS;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyMaxHealth;
        enemyHealthIndicator.maxValue = enemyMaxHealth;
        enemyHealthIndicator.value = currentHealth;
        enemyAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onFire && Time.time > nextBurn)
        {
            addDamage(burnDamage);
            nextBurn += burnInterval;
        }
        if (onFire && Time.time > endBurn)
        {
            onFire = false;
            burnEffect.SetActive(false);
        }
    }

    public void addDamage(float damage)
    {
        enemyHealthIndicator.gameObject.SetActive(true);
        damage = damage * damageModifier;
        if (damage <= 0f) return;
        currentHealth -= damage;
        enemyHealthIndicator.value = currentHealth;
        enemyAS.Play();
        Debug.Log("Current Health: " + currentHealth);
        if (currentHealth <= 0) makeDead();
    }

    public void damageFX(Vector3 point, Vector3 rotation)
    {
        Instantiate(damageParticles, point, Quaternion.Euler(rotation));
    }

    public void addFire()
    {
        if (!canBurm) return;
        onFire = true;
        burnEffect.SetActive(true);
        endBurn = Time.time + burmTime;
        nextBurn = Time.time + burnInterval;
    }

    void makeDead()
{
    // Store the position and rotation before destroying the enemy
    Vector3 enemyPosition = transform.position;
    Quaternion enemyRotation = transform.rotation;
    AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.15f);
    Destroy(gameObject.transform.root.gameObject);
    
    if (drops) 
    {
        // Instantiate the drop using the enemy's position and rotation
        GameObject instantiatedDrop = Instantiate(drop, enemyPosition, Quaternion.identity);

        // Set the drop's position to match the enemy
        instantiatedDrop.transform.position = enemyPosition;

        // Set the drop's rotation to match the enemy
        instantiatedDrop.transform.rotation = enemyRotation;
    }

    
}


}
