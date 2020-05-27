using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityAtoms.BaseAtoms;

public class UIPointsMeter : MonoBehaviour
{
    public IntVariable points;

    public void UpdatePoints() {
        GetComponent<TextMeshProUGUI>().SetText(points.Value.ToString());
    }
}
