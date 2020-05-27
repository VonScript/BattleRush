using UnityEngine;
using System.Collections;
using UnityAtoms.BaseAtoms;
using UnityEngine.UI;

public class UIRushCDMeter : MonoBehaviour
{
    public IntVariable rush_cooldown;
    public Image rush_pool;

    public void Cooldown() {
        rush_pool.fillAmount = 1.0f - 1.0f * rush_cooldown.Value / 10;
    }
}
