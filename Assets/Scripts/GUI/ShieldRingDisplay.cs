using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldRingDisplay : MonoBehaviour
{
    public Sprite[] bars = new Sprite[11];
    Image display;
    int TOTAL_BARS = 10;

    private void Awake()
    {
        display = GetComponent<Image>();
        display.sprite = bars[10];
    }

    public void SetPercentages(float current, float max)
    {
        int barCount = (int)Mathf.Round((current / max) * TOTAL_BARS);
        display.sprite = bars[barCount];
    }
}
