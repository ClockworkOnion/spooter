using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFields : MonoBehaviour
{
    public GameObject[] decorationObjects;
    public float EXTENDS = 300f;
    public float Z_OFFSET = 20f;
    public float Z_EXTENDS = 40f;
    public int asteroidCount = 100;
    private Transform playerPosition;
    private float teleportDistance;

    void Start()
    {
        teleportDistance = EXTENDS * 3.8f;
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        for (int i = 0; i <= asteroidCount; i++)
        {
            float xPos = Random.Range(-EXTENDS, EXTENDS);
            float yPos = Random.Range(-EXTENDS, EXTENDS);
            float zPos = Random.Range(Z_OFFSET, Z_OFFSET + Z_EXTENDS);
            Vector3 newPosition = new Vector3(xPos, yPos, zPos);
            float quaternion_w = Random.Range(100, 200);
            Quaternion randRotation = new Quaternion(xPos, yPos, zPos, quaternion_w);
            GameObject newAsteroid = Instantiate(RandomObject(), transform.position+newPosition, randRotation);
            newAsteroid.transform.parent = gameObject.transform;
        }

        AsteroidFields[] fields = FindObjectsOfType<AsteroidFields>();
        if (fields.Length > 1) return; // Only the first field may instantiate others!

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j<= 1; j++)
            {
                if (!(i == 0 && j == 0))
                {
                    Instantiate(gameObject, new Vector3(transform.position.x + i*EXTENDS * 2, transform.position.y + j*EXTENDS*2, transform.position.z), Quaternion.identity);
                }
            }
        }
    }


    private GameObject RandomObject()
    {
        int l = decorationObjects.Length;
        return decorationObjects[Random.Range(0, l)];
    }

    void Update()
    {
        if (playerPosition.position.x - transform.position.x > teleportDistance)
        {
            transform.position = transform.position + new Vector3(6 * EXTENDS, 0, 0);
        }

        if (playerPosition.position.x - transform.position.x < -teleportDistance)
        {
            transform.position = transform.position + new Vector3(-6 * EXTENDS, 0, 0);
        }

        if (playerPosition.position.y - transform.position.y > teleportDistance)
        {
            transform.position = transform.position + new Vector3(0, 6 * EXTENDS, 0);
        }

        if (playerPosition.position.y - transform.position.y < -teleportDistance)
        {
            transform.position = transform.position + new Vector3(0, -6 * EXTENDS, 0);
        }
    }
}
