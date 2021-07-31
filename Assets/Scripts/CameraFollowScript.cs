using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(0, target.position.y, 0) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player") Destroy(collision.gameObject);
    }
}
