using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIControl : MonoBehaviour
{

    private PlayerDamageModel dmgModel;
    private ShipSystems plrShipSystems;
    private ShieldRingDisplay[] shieldsDisplay = new ShieldRingDisplay[4];
    private float[] currentShields = new float[4]; // vorne, rechts, hinten, links
    private float[] maxShields = new float[4];

    private float maxEnergy = 100;
    private float currentEnergy = 50;
    private ShieldRingDisplay energyRings;

    void Start()
    {
        plrShipSystems = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipSystems>();
        dmgModel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamageModel>();
        shieldsDisplay[0] = transform.Find("ShieldForward").GetComponent<ShieldRingDisplay>();
        shieldsDisplay[1] = transform.Find("ShieldRight").GetComponent<ShieldRingDisplay>();
        shieldsDisplay[2] = transform.Find("ShieldBackward").GetComponent<ShieldRingDisplay>();
        shieldsDisplay[3] = transform.Find("ShieldLeft").GetComponent<ShieldRingDisplay>();
        dmgModel.Shields(currentShields);
        maxShields = dmgModel.maxShield;
        energyRings = transform.Find("EngineRings").GetComponent<ShieldRingDisplay>();


    }

    void Update()
    {
        // Energy pool
        maxEnergy = plrShipSystems.maxEnergyPool;
        currentEnergy = plrShipSystems.Energy;
        energyRings.SetPercentages(currentEnergy, maxEnergy);

        // Shields
        dmgModel.Shields(currentShields);
        for (int i = 0; i < 4; i++)
        {
            shieldsDisplay[i].SetPercentages(currentShields[i], maxShields[i]);
        }
    }
}
