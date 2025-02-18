using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    /*private Transform player;
    
    void FixedUpdate() 
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        
    }*/
    public Transform target;
    public float lerpSpeed = 1.0f;

    private Vector3 offset;

    private Vector3 targetPos;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        targetPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.fixedDeltaTime);
    }

}
