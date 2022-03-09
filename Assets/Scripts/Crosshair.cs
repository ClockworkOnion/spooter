using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Camera mainCam;
    private Plane board;
    private Vector3 gunsTarget;


    void Start()
    {
        board = new Plane(Vector3.forward, Vector3.zero);
    }

    void Update()
    {
        gunsTarget = GetCrosshairTarget();
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
