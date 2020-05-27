using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public GameObject boss;
    public BoolVariable gameover;
    public IntVariable track_count;
    private Vector3 _position;

    private void Awake(){
        InvokeRepeating("Spawn", 0f, 1f);
    }

    private void Update() {
        if(gameover.Value) CancelInvoke();
    }

    private void Spawn() {
        _position = transform.position;
        int lane = Random.Range(-1, 2);
        _position.x = lane + lane;
        Instantiate(enemy, _position, Quaternion.Euler(-90, 0, 180));
    }

    public void SpawnBoss() {
        if (track_count.Value < 12 && track_count.Value % 3 == 0) {
            _position = transform.position;
            int lane = Random.Range(-1, 2);
            _position.x = lane + lane;
            Instantiate(boss, _position, Quaternion.Euler(-90, 0, 180));
        }
    }
}
