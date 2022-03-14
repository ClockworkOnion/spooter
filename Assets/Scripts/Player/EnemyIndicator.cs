using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    public GameObject indicator;
    public float distanceToPlayer = 20;
    GameObject player;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rend = GetComponentInChildren<Renderer>();
        indicator = Instantiate(indicator, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!rend.isVisible && player != null)
        {
            indicator.SetActive(true);
            indicator.transform.up = transform.position - player.transform.position;
            indicator.transform.position = player.transform.position + indicator.transform.up * distanceToPlayer;
        } else
        {
            indicator.SetActive(false);
        }
        
    }
}
