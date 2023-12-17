using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class evaletorController : MonoBehaviour
{
    public float resetTime;

    Animator elevAnim;
    AudioSource elevAs;

    float downTime;
    bool elevIsUp = false;


    // Start is called before the first frame update
    void Start()
    {
        elevAnim = GetComponent<Animator>();
        elevAs = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (downTime <= Time.time && elevIsUp)
        {
            elevAnim.SetTrigger("activateElavator");
            elevIsUp = false;
            elevAs.Play();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            elevAnim.SetTrigger("activateElavator");
            downTime = Time.time + resetTime;
            elevIsUp = true;
            elevAs.Play();
        }
    }
}
