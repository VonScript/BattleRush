using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody _enemy;

    private void Awake(){
         _enemy = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Player"){
            Debug.Log("player hit");
            Destroy(gameObject);
        }
    }

    private void Update(){
        _enemy.position += Vector3.back * speed * Time.deltaTime;
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
