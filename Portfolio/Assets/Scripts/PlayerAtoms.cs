using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PlayerAtoms : MonoBehaviour
{
    public IntVariable health;
    public IntVariable points;
    public IntVariable killstreak;
    public BoolVariable rush_activated;
    public BoolVariable rush_cooldown_active;
    public IntVariable rush_cooldown;
    public BoolVariable ult_activated;
    public BoolVariable has_jumped;
    public BoolVariable escape;
}
