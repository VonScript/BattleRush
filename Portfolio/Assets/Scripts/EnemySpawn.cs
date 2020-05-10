using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public BoolVariable gameover;
    private Vector3 _position;
    private int _stop = 4;

    void Awake(){
        InvokeRepeating("MoreSpawn", 2f, 2.5f);
    }

    private void Update() {
        if (_stop == 0) CancelInvoke();

        if(gameover.Value) CancelInvoke();
    }

    private void MoreSpawn() {
        _position = transform.position;
        int lane = Random.Range(-1, 1);
        _position.x = lane + lane;
        Instantiate(enemy, _position, Quaternion.identity);
        _stop--;
    }
}
