using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add this component to a ship to make it approach random positions around the player.
/// Recommended to put some linear drag and angular drag on the rigid body!
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAIControl : MonoBehaviour
{
    Transform playerPosition;
    public int APPROACH_DISTANCE_MIN = 25;
    public int APPROACH_DISTANCE_MAX = 70;
    [Tooltip("For adjusting the size of the targeting reticle")]
    public float shipSizeMod = 1f;
    private int approachDistanceCurrent = 25;
    private float angleToGoal = 0;
    Rigidbody2D rb;
    public float turnSpeed = 800;
    public float thrustStrength = 12f;
    public float TOP_SPEED = 2;
    public enemyTypes type = enemyTypes.baseShip;
    int goalIndex = 0;
    Vector3 movementGoal;
    Vector3[] positions = new Vector3[4];
    Vector3 lastPosition;
    [SerializeField]
    float currentSpeed = 0f;
    private ShipSystems shipSystems;
    private EnemyDamageModel dmgModel;

    private LevelManager thisLevel;

    private void Awake()
    {
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        shipSystems = GetComponent<ShipSystems>();
        dmgModel = GetComponent<EnemyDamageModel>();
    }

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        RandomizeMovementGoal();
        positions = PositionsAroundPlayer(approachDistanceCurrent);
        movementGoal = positions[goalIndex];
    }

    void Update()
    {
        if (playerPosition == null) return;
        Debug.DrawLine(transform.position, movementGoal, Color.black);
        positions = PositionsAroundPlayer(approachDistanceCurrent);
        movementGoal = positions[goalIndex]; // Update movement goal

        Vector3 vectorTowardsGoal = (movementGoal - transform.position).normalized;
        angleToGoal = Vector3.SignedAngle(vectorTowardsGoal, transform.up, Vector3.forward);

        if (Vector3.Distance(transform.position, movementGoal) < 20f)
        {
            RandomizeMovementGoal();
        }
    }

    private void FixedUpdate()
    {
        int rotateDirection = (int)-Mathf.Sign(angleToGoal);
        float rotateMultiplier = (Mathf.Abs(angleToGoal)/100) + 0.5f; // Rotate more quickly when facing away
        rb.AddTorque(rotateDirection * Time.fixedDeltaTime * turnSpeed * rotateMultiplier);

        if (currentSpeed < TOP_SPEED)
        {
            rb.AddForce(transform.up * Time.fixedDeltaTime * thrustStrength);
        }

        currentSpeed = (lastPosition - transform.position).magnitude;
        lastPosition = transform.position;
    }

    private Vector3[] PositionsAroundPlayer(int distance)
    {
        Vector3[] positions = new Vector3[4];
        positions[0] = playerPosition.position + Vector3.up * distance;
        positions[1] = playerPosition.position + -Vector3.up * distance;
        positions[2] = playerPosition.position + -Vector3.right * distance;
        positions[3] = playerPosition.position + Vector3.right * distance;
        return positions;
      }

    private void RandomizeMovementGoal()
    {
        approachDistanceCurrent = Random.Range(APPROACH_DISTANCE_MIN, APPROACH_DISTANCE_MAX);
        goalIndex = Random.Range(0, 3);
    }

    public void SetLevelManager(LevelManager l) // Called by LevelManager upon spawning the ship
    {
        thisLevel = l;
    }

    public enum enemyTypes
    {
        baseShip,
        biggerShip
    }

    private void OnDestroy() // Tell the level manager when this ship was destroyed so it can do other stuff
    {
        if (thisLevel != null) thisLevel.OnEnemyDestroyed(this);
    }

    public Dictionary<string, string> ShipInfo()
    {
        Dictionary<string, string> info = new Dictionary<string, string>();
        info.Add("name", shipSystems.shipName);
        info.Add("hull", Mathf.Round((dmgModel.Hull / dmgModel.maxHull) * 100).ToString());
        return info;
    }
}
