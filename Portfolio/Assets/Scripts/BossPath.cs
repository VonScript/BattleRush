using UnityEngine;
using System.Collections;

public class BossPath : MonoBehaviour
{
    public float rush = 30.0f;
    private Rigidbody _boss;
    private Animator _animator;

    private void Awake() {
        _boss = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            GetComponent<Rigidbody>().detectCollisions = false;
            var atoms = other.gameObject.GetComponent<PlayerAtoms>();
            atoms.health.Value -= 30;
            StartCoroutine(Hold());
        }

        if (other.gameObject.tag == "Enemy") Destroy(other.gameObject);
    }


    private void Update() {
        _boss.position += Vector3.back * rush * Time.deltaTime;
    }

    private IEnumerator Hold() {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Rigidbody>().detectCollisions = true;
    }
}
