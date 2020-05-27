using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {
    public float speed = 10.0f;
    private Rigidbody _enemy;
    private Animator _animator;

    private void Awake() {
        _enemy = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            GetComponent<Rigidbody>().detectCollisions = false;
            var atoms = other.gameObject.GetComponent<PlayerAtoms>();
            if (atoms.rush_activated.Value || atoms.has_jumped.Value || atoms.ult_activated.Value) {
                Destroy(gameObject);
            } else StartCoroutine(Hold());
        }
    }


    private void Update() {
        _enemy.position += Vector3.back * speed * Time.deltaTime;
    }

    private IEnumerator Hold() {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Rigidbody>().detectCollisions = true;
    }
}
