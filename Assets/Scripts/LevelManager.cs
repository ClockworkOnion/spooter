using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private List<EnemyAIControl> enemiesInScene = new List<EnemyAIControl>();
    public int currentEnemyCount = 0;
    public int maxEnemyCount = 3;
    public int enemyReinforcements = 5;
    Transform playerTransform;
    PlayerDamageModel playerDmgMdl;
    public GameObject[] enemyShipPrefabs;

    // Wave Mode
    public bool waveModeOn = true;
    public int waveNo = 1;
    TextMeshProUGUI waveText;
    public float timeToNextWave = 0f;
    private int totalShipsDestroyed = 0;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerDmgMdl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamageModel>();
        waveText = GameObject.Find("WaveClearText").GetComponent<TextMeshProUGUI>();
        waveText.enabled = false;

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
        if (playerTransform == null) return;
        while (currentEnemyCount < maxEnemyCount && enemyReinforcements > 0 && waveModeOn) // Respawn enemy ships until reinforcements are empty
        {
            float spawnDistance = 300f;
            Vector3 spawnPosition = new Vector3(spawnDistance * MinusOrNot(), spawnDistance * MinusOrNot(), 0);
            EnemyAIControl newEnemy = Instantiate(enemyShipPrefabs[0], playerTransform.position + spawnPosition , Quaternion.identity).GetComponent<EnemyAIControl>();
            enemiesInScene.Add(newEnemy);
            newEnemy.SetLevelManager(this);
            currentEnemyCount = enemiesInScene.Count;
            enemyReinforcements--;
        }

        if (currentEnemyCount == 0 && enemyReinforcements == 0 && waveModeOn) TriggerNextWave();
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
        totalShipsDestroyed++;
    }

    public void CreateEnemyWave(int maxEnemies, int totalEnemies)
    {
        maxEnemyCount = maxEnemies;
        enemyReinforcements = totalEnemies;
    }

    private void TriggerNextWave()
    {
        StartCoroutine(HideWaveText(4));
        waveNo++;
        enemyReinforcements = (waveNo + MinusOrNot())*2;
        maxEnemyCount = Mathf.Min((waveNo + MinusOrNot()), enemyReinforcements);
        waveText.enabled = true;
        waveText.text = "Wave " + waveNo.ToString() +  " Cleared! \n  Shields and Energy restored. \n\n  Now try the next wave:\n" + maxEnemyCount.ToString() + " at once, " + enemyReinforcements.ToString() + " in total.";
        playerDmgMdl.RechargeShields(1, 100);
        playerDmgMdl.RepairHull(100);
    }

    IEnumerator HideWaveText(float timer)
    {
        yield return new WaitForSeconds(timer);
        waveText.enabled = false;
    }

    public List<EnemyAIControl> getActiveEnemies()
    {
        return enemiesInScene;
    }

}
