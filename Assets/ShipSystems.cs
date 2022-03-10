using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystems : MonoBehaviour
{
    public float maxEnergyPool = 100;
    public float energyPool = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float Energy
    {
        get => energyPool;
    }
}
