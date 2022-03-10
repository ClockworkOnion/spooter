using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystems : MonoBehaviour
{
    public float maxEnergyPool = 100;
    public float energyPool = 100;
    public float energyRegeneration = 6f;

    private AudioSource engineSound;
    private float engineVolume = 0f;

    void Awake()
    {
        engineSound = GetComponent<AudioSource>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (energyPool < maxEnergyPool)
        {
            energyPool += energyRegeneration * Time.deltaTime;
        }
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
