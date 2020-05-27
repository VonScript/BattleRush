using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using UnityAtoms.BaseAtoms;

public class PlayerAbilities : MonoBehaviour
{
    public BoolVariable gameover;

    private PlayerAtoms _atoms;
    private bool _is_hit = false;
    private int _blink = 0;
    private Color _og_colour;

    private void Awake(){
        _atoms = GetComponent<PlayerAtoms>();
        _og_colour = GetComponent<Renderer>().material.color;
    }

    private void OnCollisionEnter(Collision other){
        if (other.gameObject.tag == "Enemy") {
            if (!_atoms.rush_activated.Value && !_atoms.has_jumped.Value && !_atoms.ult_activated.Value) {
                if(!_is_hit) { _is_hit = true; InvokeRepeating("Blink", 0, .1f); if (_atoms.killstreak.Value != 10) _atoms.killstreak.Value = 0; }
                _atoms.health.Value -= 10;
            } else {
                if (_atoms.rush_activated.Value) _atoms.points.Value += 5;
                if (_atoms.has_jumped.Value) _atoms.points.Value += 15;
                if (_atoms.ult_activated.Value) _atoms.points.Value += 50;
                if (_atoms.killstreak.Value < 10) _atoms.killstreak.Value += 1;
            }
        }
    }

    private void Blink() {
        Color colour = GetComponent<Renderer>().material.color;
        if (colour.a == 1) colour = new Color(1f, 1f, 1f, 0); else colour = _og_colour;
        GetComponent<Renderer>().material.color = colour;
        _blink++;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_atoms.has_jumped.Value) {
            if (!_atoms.rush_activated.Value && !_atoms.rush_cooldown_active.Value && !_atoms.ult_activated.Value) {
                _atoms.rush_activated.Value = true;              
                gameObject.AddComponent<AbilityRush>();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && _atoms.killstreak.Value == 10) {
            if (!_atoms.rush_activated.Value && !_atoms.has_jumped.Value && !_atoms.ult_activated.Value) {
                UltimateActivated();
            }
        }

        //end game
        if (Input.GetKeyDown(KeyCode.Escape)) { _atoms.escape.Value = true; _atoms.health.Value = 0; }

        if (_atoms.health.Value == 0) { Destroy(gameObject); GameOver(); }

        if (_blink == 10) { CancelInvoke(); _is_hit = false; _blink = 0; }
    }

    private void GameOver() => gameover.Value = true;

    public void UltimateActivated() {
        if (!_atoms.ult_activated.Value) {
            _atoms.ult_activated.Value = true;
            _atoms.rush_activated.Value = true;
            gameObject.AddComponent<AbilitySlam>();
        }
    }
}
