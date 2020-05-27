using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class TrackSpawn : MonoBehaviour
{
    public GameObject track;
    public GameObject finishline;
    public IntVariable track_count;
    private Vector3 _position;  

    private void Awake() {
        _position = transform.position;
        _position.z += 138.0f;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            if (gameObject.transform.parent.name == "12") finishline.SetActive(true);

            if (track_count.Value < 15) {
                var asset = Instantiate(track, _position, Quaternion.identity);
                asset.name = track_count.Value.ToString();
                track_count.Value++;
                gameObject.SetActive(false);
            }

        }
    }
}
