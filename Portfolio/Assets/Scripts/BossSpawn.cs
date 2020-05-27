using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject boss;
    public IntVariable track_count;
    private Vector3 _position;

    public void Spawn() {
        if (track_count.Value < 12 && track_count.Value % 3 == 0) {
            _position = transform.position;
            int lane = Random.Range(-1, 2);
            _position.x = lane + lane;
            Instantiate(boss, _position, Quaternion.Euler(-90, 0, 180));
        }
    }
}
