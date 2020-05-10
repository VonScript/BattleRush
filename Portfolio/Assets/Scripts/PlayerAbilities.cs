using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine.SceneManagement;

public class PlayerAbilities : MonoBehaviour
{
    public IntVariable health;
    public BoolVariable rush_activated;
    public BoolVariable has_jumped;
    public IntVariable points;
    public BoolVariable gameover;
    private Rigidbody _rb;

    void Awake(){
        _rb = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy") {
            if (!rush_activated.Value && !has_jumped.Value) health.Value -= 15; else points.Value += 15;
        }
    }

    void FixedUpdate()
    {
        if (health.Value == 0) { Destroy(gameObject);  GameOver(); }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (!rush_activated.Value) {
                rush_activated.Value = true;
                gameObject.AddComponent<AbilityRush>();
            }
        }

        //end game
        if (Input.GetKeyDown(KeyCode.Escape)) health.Value = 0;
    }

    void GameOver() {
        gameover.Value = true;
    }
}
