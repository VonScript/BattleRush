using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This whole tiny a$$ script exists because OnBecameInvisible is being a B*tch.
/// </summary>

public class EnemyDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") Destroy(other.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Track") Destroy(other.gameObject); 
    }
}