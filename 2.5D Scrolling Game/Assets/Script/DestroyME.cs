using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyME : MonoBehaviour
{
    public float aliveTime;
    public bool destroyOnAwake;



    // Start is called before the first frame update
    public void Awake()
    {
        if (destroyOnAwake != false)
        {
            Destroy (gameObject, aliveTime);
        }         
    }
}
