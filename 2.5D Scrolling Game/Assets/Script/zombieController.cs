using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieController : MonoBehaviour
{
    public GameObject flipModel;
    public GameObject ragdollDead;

    //audio option
    public AudioClip[] idleSound;
    public float idleSoundTime;
    AudioSource enemyMovementAS;
    float nextIdleSound = 0f;

    public float detectionTime;
    float startRun;
    bool firstDetection;

    //movement option
    public float runSpeed;
    public float walkSpeed;
    public bool facingRight = true;
    
    float moveSpeed;
    bool running;

    Rigidbody myRB;
    Animator myAnim;
    Transform detectedPlayer;
    
    bool Detected;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponentInParent<Rigidbody>();
        myAnim = GetComponentInParent<Animator>();
        enemyMovementAS = GetComponent<AudioSource>();

        running = false;
        Detected = false;
        firstDetection = false;
        moveSpeed = walkSpeed;

        if (Random.Range(0, 10)>5 )
        {
            Flip();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Detected)
        {
            if (detectedPlayer.position.x < transform.position.x && facingRight)Flip();
            else if(detectedPlayer.position.x > transform.position.x && !facingRight)Flip();

            if (!firstDetection)
            {
                startRun = Time.time + detectionTime;
                firstDetection = true;
            }
        }
        if (Detected && !facingRight)
        {
            myRB.velocity = new Vector3(moveSpeed *-1, myRB.velocity.y, 0); 
        }
        else if(Detected && facingRight)
        {
            myRB.velocity = new Vector3(moveSpeed, myRB.velocity.y, 0);
        }

        if (!running && Detected)
        {
            if(startRun<Time.time){
                moveSpeed = runSpeed;
                myAnim.SetTrigger("run");
                running = true;
            }
        }
        //idle or walking sound
        if (!running)
        {
            if(Random.Range(0,10)> 5 && nextIdleSound<Time.time){
                    AudioClip tempClip = idleSound[Random.Range(0, idleSound.Length)];
                    enemyMovementAS.clip = tempClip;
                    enemyMovementAS.Play();
                    nextIdleSound = idleSoundTime + Time.time;

            }
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.tag =="Player" && !Detected)
        {
            Detected = true;
            detectedPlayer = other.transform;
            myAnim.SetBool("detected",Detected);
            if (detectedPlayer.position.x < transform.position.x && facingRight)Flip();
            else if(detectedPlayer.position.x > transform.position.x && !facingRight)Flip();

        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            firstDetection = false;
            if(running){
                myAnim.SetTrigger("run");
                moveSpeed = walkSpeed;
                running = false;
            }
        }
    }

    void Flip(){
        facingRight = !facingRight;
        Vector3 theScale = flipModel.transform.localScale;
        theScale.z *= -1;
        flipModel.transform.localScale = theScale;

    }

    public void ragDollDeath(){
        GameObject ragdoll = Instantiate(ragdollDead, transform.root.transform.position, Quaternion.identity) as GameObject; 

        Transform ragDollMaster = ragdoll.transform.Find("master");
        Transform zombieMaster = transform.root.Find("master");

        bool wasFacinigRight = true;
        if (!facingRight)
        {
            wasFacinigRight = false;
            Flip();
        }

        Transform[] ragdollJoints = ragDollMaster.GetComponentsInChildren <Transform>();
        Transform[] currentJoints = zombieMaster.GetComponentsInChildren<Transform>();

        for (int i = 0; i < ragdollJoints.Length; i++)
        {
            for (int q = 0; q < currentJoints.Length; q++)
            {
                if (currentJoints[q].name.CompareTo(ragdollJoints[i].name)==0)
                {
                    ragdollJoints[i].position = currentJoints[q].position;
                    ragdollJoints[i].rotation = currentJoints[q].rotation;
                }
            }
        }

        if (wasFacinigRight)
        {
            Vector3 rotVector = new Vector3(0,0,0);
            ragdoll.transform.rotation = Quaternion.Euler(rotVector);
        }
        else
        {
            Vector3 rotVector = new Vector3(0,90,0);
            ragdoll.transform.rotation = Quaternion.Euler(rotVector);

        }

        Transform zombieMesh = transform.root.transform.Find ("zombieSoldier");
        Transform ragdollMesh = ragdoll.transform.Find("zombieSoldier");

        ragdollMesh.GetComponent<Renderer>().material = zombieMesh.GetComponent<Renderer>().material;

    }
}
