using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public float ROTATION_SPEED = 90f;
    private Transform model;
    private Renderer render;
    // Start is called before the first frame update
    void Start()
    {
        model = transform.Find("Model").GetComponent<Transform>();
        render = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        model.Rotate(new Vector3(0, 0, ROTATION_SPEED*Time.deltaTime));
    }

    public void Show()
    {
        render.enabled = true;
    }

    public void Hide()
    {
        render.enabled = false;
    }
}
