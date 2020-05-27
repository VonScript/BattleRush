using UnityEngine;
using System.Collections;
using UnityAtoms.BaseAtoms;
using UnityEngine.UI;
using TMPro;

public class UISlamMeter : MonoBehaviour {

    public IntVariable killstreak;
    public BoolVariable ultimate_activated;
    public Image slam_pool;
    public TextMeshProUGUI killstreak_text;

    public void Tally() {
        if (killstreak.Value != 10) {
            if (slam_pool.color.r == 1) slam_pool.color = new Color(1, 1, 1);
        } else slam_pool.color = new Color(1, 0, 0);

        slam_pool.fillAmount = 1.0f * killstreak.Value / 10;
        killstreak_text.SetText(killstreak.Value.ToString());
    }

    public void SlamButton() {
        if (ultimate_activated.Value) killstreak_text.SetText(">:(");
    }
}
