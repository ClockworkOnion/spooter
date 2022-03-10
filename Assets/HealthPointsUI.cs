using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointsUI : MonoBehaviour
{
    [Range(0f, 1000f)]
    public int healthPoints;
    public Text healthPointsText;

    float[] shieldsPoints = new float[4];
    public Text[] shieldsTexts = new Text[4];

    public GameObject player;
    public DamageModel damageModel;


    void Start()
    {
        player = GameObject.Find("Player");
        damageModel = player.GetComponent<DamageModel>();

        healthPointsText.text = "HP: " + damageModel.Hull.ToString("0");

        shieldsPoints = damageModel.Shields(shieldsPoints);

        for (int i = 0; i < shieldsPoints.Length; i++)
        {
            shieldsTexts[i].text = "Shield " + (i + 1) + ": " + shieldsPoints[i];
        }
    }

 
    void Update()
    {
        healthPointsText.text = "HP: " + damageModel.Hull.ToString("0");

        damageModel.Shields(shieldsPoints);

        for (int i = 0; i < shieldsPoints.Length; i++)
         {
             shieldsTexts[i].text = "Shield " + (i + 1) + ": " + shieldsPoints[i].ToString("0");
         }
    }
}
