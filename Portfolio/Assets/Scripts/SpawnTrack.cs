using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class SpawnTrack : MonoBehaviour
{
    public GameObject track;
    private Vector3 _position;

    private void Awake() {
        _position = transform.position;
        _position.z += 50.0f;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            Instantiate(track, _position, Quaternion.identity);
        }
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
