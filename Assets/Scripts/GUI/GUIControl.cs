using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIControl : MonoBehaviour
{

    private GameObject player;
    private PlayerDamageModel dmgModel;
    private Crosshair crosshair;
    private TargetIndicator targetIndicator;
    private LevelManager levelmanager;
    private ShipSystems plrShipSystems;
    private LaserCannon laser;
    private GuidedMissileLauncher guidedMissiles;
    private MissleLauncher missiles;
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
        player = GameObject.FindGameObjectWithTag("Player");
        plrShipSystems = player.GetComponent<ShipSystems>();
        dmgModel = player.GetComponent<PlayerDamageModel>();
        laser = player.GetComponent<LaserCannon>();
        guidedMissiles = player.transform.Find("Tower").GetComponent<GuidedMissileLauncher>();
        missiles = player.GetComponent<MissleLauncher>();

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
        if (plrShipSystems == null) return;

        // Speed Display
        Vector2 vel = plrShipSystems.GetComponent<Rigidbody2D>().velocity;
        string energy = plrShipSystems.Energy.ToString();
        string maxnrg_string = plrShipSystems.maxEnergyPool.ToString();
        string hull = dmgModel.Hull.ToString();
        string maxHull = dmgModel.maxHull.ToString();
        string maxShield = dmgModel.maxShield.ToString();
        string laserRate = laser.refireRate.ToString();
        string laserDmg = laser.damage.ToString();
        string missileRate = missiles.refireRate.ToString();
        string missilePrecision = guidedMissiles.missilePrecision.ToString();
           
        speedText.SetText($"Speed:\n{vel.ToString()}\nTotal:\n {vel.magnitude}\nEnergy: [{energy}/{maxnrg_string}]\nHull:[{hull}/{maxHull}]\nMaxShield:{maxShield}\nLaser Fire Rate:{laserRate}\nLaser Power:{laserDmg}\nMissile Fire Rate:{missileRate}\nMissile Precision:{missilePrecision}");


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
            targetIndicator.transform.localScale = new Vector3(closestEnemy.shipSizeMod, closestEnemy.shipSizeMod, closestEnemy.shipSizeMod);

        } else
        {
            infoText.SetText("Scanning...");
            targetIndicator.Hide();
        }
    }

}
