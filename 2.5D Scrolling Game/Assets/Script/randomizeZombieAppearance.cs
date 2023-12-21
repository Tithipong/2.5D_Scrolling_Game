using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomizeZombieAppearance : MonoBehaviour
{
    public Material[] zombieMaterails;


    // Start is called before the first frame update
    void Start()
    {
        SkinnedMeshRenderer myRenderer = GetComponent<SkinnedMeshRenderer>();
        myRenderer.material = zombieMaterails[Random.Range(0,zombieMaterails.Length)];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
