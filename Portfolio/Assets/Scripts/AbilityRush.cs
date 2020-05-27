using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class AbilityRush : MonoBehaviour
{
    private enum State { Start, Run, Cooldown, End }
    private State _state;
    private int _countdown_timer;

    void FixedUpdate() {

        switch (_state) {

            case State.Start:
                _countdown_timer = 6;
                _state = State.Run;
                InvokeRepeating("Countdown", 0, 0.5f);
                break;

            case State.Run:
                if (_countdown_timer == 0) {
                    CancelInvoke();
                    GetComponent<PlayerAtoms>().rush_activated.Value = false;
                    GetComponent<PlayerAtoms>().rush_cooldown_active.Value = true;
                    _state = State.Cooldown;
                    GetComponent<PlayerAtoms>().rush_cooldown.Value = 10;
                    InvokeRepeating("Cooldown", 0, 1f);
                }

                break;

            case State.Cooldown:
                if (GetComponent<PlayerAtoms>().rush_cooldown.Value == 0) {
                    CancelInvoke();
                    _state = State.End;
                }
                break;

            case State.End:
                GetComponent<PlayerAtoms>().rush_cooldown_active.Value = false;
                Destroy(this);
                break;
        }

    }

    private void Countdown() => _countdown_timer--;
    private void Cooldown() => GetComponent<PlayerAtoms>().rush_cooldown.Value--;
    
}
