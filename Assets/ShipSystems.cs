using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystems : MonoBehaviour
{
    public string shipName = "defaultShip";
    public float maxEnergyPool = 100;
    [SerializeField]
    private float energyPool = 100;
    public float energyRegeneration = 1f;

    private AudioSource engineSound;
    private float engineVolume = 0f;

    private ShieldRingDisplay engineRings;
    [SerializeField]
    private Vector3 movementVector;

    void Awake()
    {
    }

    void Start()
    {
        engineSound = transform.Find("EngineTrailLeft").GetComponent<AudioSource>();
        engineRings = GameObject.Find("EngineRings").GetComponent<ShieldRingDisplay>();
    }

    void Update()
    {
        if (energyPool < maxEnergyPool)
        {
            energyPool += energyRegeneration * Time.deltaTime;
        }
        if (gameObject.tag != "Player") return; // Player only stuff from here on (no enemy)
        HandleEngineVolume();

    }

  
    public float Energy
    {
        get => energyPool;
    }

  
    public bool useEnergy(float amount)
    {
        if (amount < energyPool)
        {
            energyPool -= amount;
            return true;
        }
        return false;
    }

    void HandleEngineVolume()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            engineVolume = Mathf.Lerp(engineVolume, 1, 0.2f);
        } else {
            engineVolume = Mathf.Lerp(engineVolume, 0, 0.3f);
        }
        engineSound.volume = engineVolume;
    }
    
   }
