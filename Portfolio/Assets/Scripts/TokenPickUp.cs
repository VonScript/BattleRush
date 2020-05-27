using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenPickUp : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<PlayerAtoms>().points.Value += 50;
            Destroy(gameObject);
        }
    }
}
