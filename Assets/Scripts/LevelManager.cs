using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private List<EnemyAIControl> enemiesInScene = new List<EnemyAIControl>();
    public int currentEnemyCount = 0;
    public int maxEnemyCount = 3;
    public int enemyReinforcements = 5;
    Transform playerTransform;
    public GameObject[] enemyShipPrefabs;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (enemyShipPrefabs.Length == 0) Debug.Log("Forgot to put prefabs into LevelManager?");

        // Fill list to keep track of enemy ships
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject o in enemies)
        {
            enemiesInScene.Add(o.GetComponent<EnemyAIControl>());
        }
        currentEnemyCount = enemiesInScene.Count;
    }

    void Update()
    {
        while (currentEnemyCount < maxEnemyCount && enemyReinforcements > 0) // Respawn enemy ships until reinforcements are empty
        {
            float spawnDistance = 300f;
            Vector3 spawnPosition = new Vector3(spawnDistance * MinusOrNot(), spawnDistance * MinusOrNot(), 0);
            EnemyAIControl newEnemy = Instantiate(enemyShipPrefabs[0], playerTransform.position + spawnPosition , Quaternion.identity).GetComponent<EnemyAIControl>();
            enemiesInScene.Add(newEnemy);
            newEnemy.SetLevelManager(this);
            currentEnemyCount = enemiesInScene.Count;
            enemyReinforcements--;
        }
    }

    private int MinusOrNot()
    {
        int number = Random.Range(0, 2);
        return (number == 1) ? 1 : -1;
    }

    public void OnEnemyDestroyed(EnemyAIControl enm) // Called by enemies when they are destroyed
    {
        enemiesInScene.Remove(enm);
        currentEnemyCount = enemiesInScene.Count;
    }

    public void CreateEnemyWave(int maxEnemies, int totalEnemies)
    {
        maxEnemyCount = maxEnemies;
        enemyReinforcements = totalEnemies;
    }

}
