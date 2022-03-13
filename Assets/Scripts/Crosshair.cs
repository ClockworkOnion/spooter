using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Camera mainCam;
    [Tooltip("How near to an enemy the tracking should lock on")]
    public float TARGETING_DISTANCE = 20.0f;
    private Plane board;
    private Vector3 gunsTarget;
    private LevelManager levelmanager;
    private EnemyAIControl closestEnemy;


    void Start()
    {
        board = new Plane(Vector3.forward, Vector3.zero);
        levelmanager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void Update()
    {
        gunsTarget = GetCrosshairTarget();
        closestEnemy = FindClosestEnemy();
    }

    Vector3 GetCrosshairTarget()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        if (board.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            transform.position = hitPoint;
            return hitPoint;
        }
        return Vector3.zero;
    }

    Vector3 getGunsTarget() { return gunsTarget; }

    private EnemyAIControl FindClosestEnemy()
    {
        List<EnemyAIControl> enemies = levelmanager.getActiveEnemies();
        EnemyAIControl closest = null;
        float nearestDistance = TARGETING_DISTANCE;
        foreach (EnemyAIControl enm in enemies)
        {
            // Check if it's closer than 30 and/or closer than current closest, if yes write it over
            float distance = Vector3.Distance(transform.position, enm.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                closest = enm;
            }
        }
        return closest;
    }

    public EnemyAIControl ClosestEnemy()
    {
        return closestEnemy;
    }
}
