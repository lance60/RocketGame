using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float speed = 1f;

    private Transform targetTransform;

    void Start()
    {
        targetTransform = target.transform;
    }

    private void FixedUpdate()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        transform.LookAt(targetTransform, Vector3.forward);
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
    }
}
