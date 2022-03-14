using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIControl : MonoBehaviour
{

    private PlayerDamageModel dmgModel;
    private Crosshair crosshair;
    private TargetIndicator targetIndicator;
    private LevelManager levelmanager;
    private ShipSystems plrShipSystems;
    private ShieldRingDisplay[] shieldsDisplay = new ShieldRingDisplay[4];
    private float[] currentShields = new float[4]; // vorne, rechts, hinten, links
    private float[] maxShields = new float[4];

    private float maxEnergy = 100;
    private float currentEnergy = 50;
    private ShieldRingDisplay energyRings;
    private TextMeshProUGUI infoText, speedText;
    private RectTransform infoRect;
    private Vector3 infoIdlePos;

    void Start()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
        targetIndicator = GameObject.Find("TargetIndicator").GetComponent<TargetIndicator>();
        levelmanager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        plrShipSystems = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipSystems>();
        dmgModel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamageModel>();
        shieldsDisplay[0] = transform.Find("ShieldForward").GetComponent<ShieldRingDisplay>();
        shieldsDisplay[1] = transform.Find("ShieldRight").GetComponent<ShieldRingDisplay>();
        shieldsDisplay[2] = transform.Find("ShieldBackward").GetComponent<ShieldRingDisplay>();
        shieldsDisplay[3] = transform.Find("ShieldLeft").GetComponent<ShieldRingDisplay>();
        energyRings = transform.Find("EngineRings").GetComponent<ShieldRingDisplay>();
        infoText = transform.Find("InfoText").GetComponent<TextMeshProUGUI>();
        speedText = transform.Find("SpeedText").GetComponent<TextMeshProUGUI>();
        infoRect = transform.Find("InfoText").GetComponent<RectTransform>();
        infoIdlePos = infoRect.position;

        dmgModel.Shields(currentShields); // Initial Shiels Info
        maxShields = dmgModel.maxShield; // Maxshields doesn't update
    }

    void Update()
    {
        // Speed Display
        speedText.SetText("Speed:\n" + plrShipSystems.GetComponent<Rigidbody2D>().velocity.ToString());


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

        // Targeting
        EnemyAIControl closestEnemy = crosshair.ClosestEnemy();
        if (closestEnemy != null)
        {
            Dictionary<string, string> info = closestEnemy.ShipInfo();
            infoText.SetText("Type: " + info["name"] + "\nHull Integrity: " + info["hull"] + "%");
            targetIndicator.Show();
            targetIndicator.transform.position = closestEnemy.transform.position;

        } else
        {
            infoText.SetText("Scanning...");
            targetIndicator.Hide();
        }
    }

}
