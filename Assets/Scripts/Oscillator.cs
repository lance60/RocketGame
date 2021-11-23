using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField]
    private Vector3 amplitude = new Vector3(0, 1, 0);

    [SerializeField]
    private Vector3 period = new Vector3(0, 1, 0);

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        Vector3 offsetPosition = CalculateOffsetPosition();
        transform.position = initialPosition + offsetPosition;
    }

    private Vector3 CalculateOffsetPosition()
    {
        float newX = 0;
        float newY = 0;
        float newZ = 0;

        if (period.x != 0)
        {
            newX = amplitude.x * Mathf.Sin(Time.time / period.x);
        }

        if (period.y != 0)
        {
            newY = amplitude.y * Mathf.Sin(Time.time / period.y);
        }

        if (period.z != 0)
        {
            newZ = amplitude.z * Mathf.Sin(Time.time / period.z);
        }


        return new Vector3(newX, newY, newZ);
    }
}
