using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Camera mainCam;
    private Plane board;
    private Vector3 gunsTarget;

    [Range(0.0f, 360.0f)]
    public float firingAngle = 90;
    public GameObject projectileType; // The gameobject/prefab that will be shot by this cannon

    void Start()
    {
        board = new Plane(Vector3.forward, Vector3.zero);
    }

    void Update()
    {
        gunsTarget = GetCrosshairTarget();

        if (Input.GetMouseButton(0))
        {
            
        }
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
}
