using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public float fullHealth;
    public float currentHealth;
    public GameObject playerDeathFX;

    //HUD
    public Slider playerHealthSlider;
    public Image damageScreen;
    public Canvas gameOverScreen;
    Color flashColor = new Color(255F, 255f, 255f, 1f);
    private float flashSpeed = 5f;
    private bool damaged = false;
    private AudioSource playerAS;

    // Start is called before the first frame update
    public void Awake()
    {
        currentHealth = fullHealth;
        playerHealthSlider.maxValue = fullHealth;
        playerHealthSlider.value = currentHealth;
        playerAS = GetComponent<AudioSource>();
    }
    public void Start()
    {
        gameOverScreen.enabled = false;
    }

    public void Update()
    {
        //if hurt
        if (damaged)
        {
            damageScreen.color = flashColor;
        }
        else
        {
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }
    public void addDamage(float damage)
    {
        if (currentHealth != 0)
        {
            currentHealth -= damage;
            playerHealthSlider.value = currentHealth;
            damaged = true;
            playerAS?.Play();
            if (playerAS != null)
            {
                playerAS.Play();
            }
            else
            {
                Debug.LogError("AudioSource is null");
            }
        }

        if (currentHealth <= 0)
        {
            makeDead();
        }
    }
    public void addHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > fullHealth)
        {
            currentHealth = fullHealth;
            playerHealthSlider.value = currentHealth;
        }
    }
    public void makeDead()
    {
        Debug.Log("Player Dead");
        Instantiate(playerDeathFX, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Player is null");
        }
        gameOverScreen.enabled = true;
    }

}
