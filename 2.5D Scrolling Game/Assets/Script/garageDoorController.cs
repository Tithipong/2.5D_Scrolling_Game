using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class garageDoorController : MonoBehaviour
{
    public bool resetable;
    public GameObject door;
    public GameObject gear;
    public bool startOpen;
    bool firstTriger = false;
    bool open = true;
    Animator doorAnim;
    Animator gearAnim;
    AudioSource doorAudio;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = door.GetComponent<Animator>();
        gearAnim = gear.GetComponent<Animator>();
        doorAudio = door.GetComponent<AudioSource>();

        if (!startOpen)
        {
            open = false;
            doorAnim.SetTrigger("doorTrigger");
            doorAudio.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !firstTriger)
        {
            if (!resetable) firstTriger = true;
            doorAnim.SetTrigger("doorTrigger");
            open = !open;
            gearAnim.SetTrigger("gearRotating");
            doorAudio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
