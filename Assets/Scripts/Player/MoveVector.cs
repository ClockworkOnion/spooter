using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVector : MonoBehaviour
{

    Rigidbody2D body;
    public LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPositions(new Vector3[] { transform.position, new Vector3(transform.position.x + body.velocity.x, transform.position.y + body.velocity.y, 0)});
    }
}
