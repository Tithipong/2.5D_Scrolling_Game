using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countTime : MonoBehaviour
{
    public Slider timeSlider;
    public float timeLimit;

    [SerializeField] private bool timeIsUp;
    private PlayerHealth playerHealth;

    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        timeSlider.maxValue = timeLimit;
        timeSlider.value = timeLimit;
        timeIsUp = true;
    }

    public void Update()
    {
        if (timeIsUp != false)
        {
            timeCountDown();
        }
        else
        {
            Destroy(this);
        }
    }
    public void timeCountDown()
    {
        timeSlider.value -= Time.deltaTime;
        if (timeSlider.value <= 0)
        {
            playerHealth.makeDead();
            timeIsUp = false;
        }
        if (playerHealth.currentHealth <= 0)
        {
            timeIsUp = false;
        }
    }
}
