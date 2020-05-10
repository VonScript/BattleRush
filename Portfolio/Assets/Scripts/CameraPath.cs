using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;


public class CameraPath : MonoBehaviour
{ 
    public float speed = 5.0f;
    public float rush = 10.0f;
    public BoolVariable rush_activated;

    void FixedUpdate()
    {
        if (!rush_activated.Value)
            transform.position += Vector3.forward * speed * Time.deltaTime;
        else
            transform.position += Vector3.forward * rush * Time.deltaTime;
    }
}
