using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : DirectionalDamage
{
    private bool leftShip = false;
    public float damage = 6f;
    private float activeTime = 0f;
    public float LIFESPAN = 5f;
    public float turnSpeed = 800f;
    public float thrustStrength = 12f;
    public float TOP_SPEED = 4f;
    public float attentionSpan = 1f;
    public GameObject explosionPrefab;
    private Transform target;
    private Vector3 movementGoal;
    private float angleToGoal;
    private float currentSpeed;

    private Vector3 lastPosition;
    private Rigidbody2D rb;
    private Collider2D ship;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        LIFESPAN += Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        activeTime += Time.deltaTime;
        if (activeTime > 0.5f) leftShip = true;
        if (activeTime > LIFESPAN)
        {
            DestroySelf();
            return;
        }

        if (target == null) return;
        Vector3 vectorTowardsGoal = (movementGoal - transform.position).normalized;
        angleToGoal = Vector3.SignedAngle(vectorTowardsGoal, transform.up, Vector3.forward);
    }

    public override float GetTriggerDamage(Collider2D collider, Collider2D other)
    {
        if (leftShip)
        {
            SpawnExplosion();
            Destroy(gameObject);
            return damage;
        }
        return 0;
    }

    private void DestroySelf()
    {
        SpawnExplosion();
        Destroy(gameObject);
    }

    private void SpawnExplosion() {
        Transform expl = Instantiate(explosionPrefab, transform.position, transform.rotation).transform;
        float size = 4f;
        expl.localScale = new Vector3(size, size, size);
        StartCoroutine(DestroyGameObject(2f));
        transform.Find("Model").gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        attentionSpan -= Time.deltaTime;
        if (attentionSpan < 0 && target != null)
        {
            attentionSpan = 1f;
            reselectGoal();
        }
        if (activeTime > 0.3f && target != null)
        {
            int rotateDirection = (int)-Mathf.Sign(angleToGoal);
            float rotateMultiplier = (Mathf.Abs(angleToGoal)/100) + 0.5f; // Rotate more quickly when facing away
            rb.AddTorque(rotateDirection * Time.fixedDeltaTime * turnSpeed * rotateMultiplier);

            if (currentSpeed < TOP_SPEED)
            {
                rb.AddForce(transform.up * Time.fixedDeltaTime * thrustStrength);
            }
        }

        currentSpeed = (lastPosition - transform.position).magnitude;
        lastPosition = transform.position;
    }

    public void SetTarget(Transform t)
    {
        target = t;
        reselectGoal();
    }

    private void reselectGoal()
    {
        movementGoal = PositionsAroundTarget(Random.Range(10f, 20f) * (LIFESPAN-activeTime-1))[Random.Range(0, 4)];
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(ship == null && !leftShip)
        {
            ship = collision;
        } else if(ship != collision)
        {
            ship = null;
            leftShip = true;
        }
        
    }

    private Vector3[] PositionsAroundTarget(float distance)
    {
        Vector3[] positions = new Vector3[5];
        positions[0] = target.position + Vector3.up * distance;
        positions[1] = target.position + -Vector3.up * distance;
        positions[2] = target.position + -Vector3.right * distance;
        positions[3] = target.position + Vector3.right * distance;
        positions[4] = target.position;
        return positions;
    }

    IEnumerator DestroyGameObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}
