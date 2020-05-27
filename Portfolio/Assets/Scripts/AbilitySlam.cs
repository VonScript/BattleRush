using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlam : MonoBehaviour
{
    private enum State { Start, Run, Cooldown, End }
    private State _state;
    private int _countdown_timer;
    private int _cooldown_timer;

    void FixedUpdate() {

        switch (_state) {

            case State.Start:
                _countdown_timer = 10;
                _cooldown_timer = 1;
                InvokeRepeating("Countdown", 0, 0.5f);
                _state = State.Run;
                break;

            case State.Run:
                if (_countdown_timer == 0) {
                    CancelInvoke();
                    InvokeRepeating("Cooldown", 0, 0.5f);
                    _state = State.Cooldown;
                }

                break;

            case State.Cooldown:
                if (_cooldown_timer == 0) {
                    CancelInvoke();
                    _state = State.End;
                }
                break;

            case State.End:
                GetComponent<PlayerAtoms>().ult_activated.Value = false;
                GetComponent<PlayerAtoms>().rush_activated.Value = false;
                GetComponent<PlayerAtoms>().killstreak.Value = 0;
                Destroy(this);
                break;
        }

    }

    private void Countdown() => _countdown_timer--;
    private void Cooldown() => _cooldown_timer--;
}
