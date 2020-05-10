using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class AbilityRush : MonoBehaviour
{ 
    public enum State { Start, Run, Cooldown, End }
    public State state;
    public int cooldownTimer;

    void FixedUpdate() {

        switch (state) {

            case State.Start:
                cooldownTimer = 2;
                state = State.Run;
                break;

            case State.Run:
                InvokeRepeating("Countdown", 0, 1.0f);
                state = State.Cooldown;
                break;

            case State.Cooldown:
                if (cooldownTimer == 0) {
                    CancelInvoke();
                    state = State.End;
                }
                break;

            case State.End:
                GetComponent<PlayerAtoms>().rush_activated.Value = false;
                Destroy(this);
                break;
        }

    }

    private void Countdown() {
        cooldownTimer--;
    }
}
