using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndShieldUI : MonoBehaviour
{
    [Range(0f, 1000f)]
    public int healthPoints;
    public Text healthPointsText;

    float[] shieldsPoints = new float[4];
    public Text[] shieldsTexts = new Text[4];

    public GameObject player;
    public DamageModel damageModel;
    public ShipSystems shipSystems;

    private Text energyPoolText;


    void Start()
    {
        player = GameObject.Find("Player");
        damageModel = player.GetComponent<DamageModel>();
        shipSystems = player.GetComponent<ShipSystems>();
        energyPoolText = GameObject.Find("EnergyPool").GetComponent<Text>();
    }

 
    void Update()
    {
        energyPoolText.text = "E: " + shipSystems.Energy.ToString("0");
        healthPointsText.text = "HP: " + damageModel.Hull.ToString("0");
        
        damageModel.Shields(shieldsPoints);
        setColors();
    }

    void setColors()
    {
        if (damageModel.Hull >= 2/3 * damageModel.maxHull)
        {
            healthPointsText.color = Color.green;
        }
        else if (damageModel.Hull >= 1 / 3 * damageModel.maxHull)
        {
            healthPointsText.color = Color.yellow;
        }
        else
        {
            healthPointsText.color = Color.red;
        }

        for (int i = 0; i < shieldsPoints.Length; i++)
        {
            shieldsTexts[i].text = "Shield " + (i + 1) + ": " + shieldsPoints[i].ToString("0");

            if (shieldsPoints[i] >= 2/3 * damageModel.maxShield[i])
            {
                shieldsTexts[i].color = Color.green;
            }
            else if (shieldsPoints[i] >= 1/3 * damageModel.maxShield[i])
            {
                shieldsTexts[i].color = Color.yellow;
            }
            else
            {
                shieldsTexts[i].color = Color.red;
            }

        }

        if (shipSystems.Energy >= 2 / 3 * shipSystems.maxEnergyPool)
        {
            energyPoolText.color = Color.green;
        }
        else if (shipSystems.Energy >= 1 / 3 * shipSystems.maxEnergyPool)
        {
            energyPoolText.color = Color.yellow;
        }
        else
        {
            energyPoolText.color = Color.red;
        }

    }
}
