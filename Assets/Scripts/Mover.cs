using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private Vector3 moveMagnitude = new Vector3(0, 0, 0);

    private void FixedUpdate()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        transform.Translate(moveMagnitude * Time.deltaTime);
    }
}
