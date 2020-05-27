using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class TokenSpawn : MonoBehaviour
{
    public GameObject token;
    public BoolVariable gameover;
    private Vector3 _position;

    void Awake() {
        InvokeRepeating("Spawn", 0f, 0.5f);
    }

    private void Update() {
        if (gameover.Value) CancelInvoke();
    }

    private void Spawn() {
        _position = transform.position;
        _position.y = 0.2f;
        int lane = Random.Range(-1, 2);
        _position.x = lane + lane;
        Instantiate(token, _position, Quaternion.identity);
    }
}
