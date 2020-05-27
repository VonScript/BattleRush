using UnityEngine;
using System.Collections;
using UnityAtoms.BaseAtoms;

public class FinishLine : MonoBehaviour
{
    public BoolVariable gameover;
    public BoolVariable finish_line;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            finish_line.Value = true;
            gameover.Value = true;
        }
    }
}
