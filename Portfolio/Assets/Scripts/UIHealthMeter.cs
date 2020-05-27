using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine.UI;

public class UIHealthMeter : MonoBehaviour
{
    public IntVariable max_health;
    public IntVariable current_health;
    public Image health_pool;

    public void ChangedHealth() {
        health_pool.fillAmount = 1.0f * current_health.Value / max_health.Value;
    }
}
