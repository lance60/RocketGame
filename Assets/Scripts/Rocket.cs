using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private KeyCode thrustKey = KeyCode.Space;

    [SerializeField]
    private KeyCode rotateClockwiseKey = KeyCode.RightArrow;

    [SerializeField]
    private KeyCode rotateCounterClockwiseKey = KeyCode.LeftArrow;

    [SerializeField]
    private float thrustMagnitude = 1f;

    [SerializeField]
    private float rotationMagnitude = 1f;

    [SerializeField]
    private AudioClip thrustSound;

    [SerializeField]
    private AudioClip deadSound;

    [SerializeField]
    private ParticleSystem thrustParticles;

    [SerializeField]
    private ParticleSystem explosionParticles;

    private Rigidbody rigidBody;
    private AudioSource audioSource;

    private float thrustIn;
    private float rotateIn;
    private bool alive;
    private bool unkillable;

    private void Start()
    {
        thrustIn = 0f;
        rotateIn = 0f;
        alive = true;
        unkillable = false;

        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        ProcessInput();
        ProcessAudio();
        ProcessParticles();
    }

    private void FixedUpdate()
    {
        ProcessMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (alive)
        {
            string tag = collision.gameObject.tag;

            switch (tag)
            {
                case "Safe":
                    break;
                case "Finish":
                    FreezePlayer();
                    unkillable = true;
                    break;
                default:
                    KillPlayer();
                    break;
            }
        }
    }

    private void ProcessInput()
    {
        if (Input.GetKey(thrustKey))
        {

            thrustIn = thrustMagnitude;
        }
        else
        {
            thrustIn = 0;
        }

        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            rotateIn = rotationMagnitude;
        }
        else if (Input.GetKey(rotateClockwiseKey))
        {
            rotateIn = -rotationMagnitude;
        }
        else
        {
            rotateIn = 0;
        }
    }

    private void ProcessMovement()
    {
        if (alive)
        {
            // Y is up
            rigidBody.AddRelativeForce(Vector3.up * thrustIn * Time.deltaTime);

            // Z is rotation axis
            rigidBody.AddRelativeTorque(Vector3.forward * rotateIn * Time.deltaTime);
        }
    }

    private void ProcessAudio()
    {
        if (alive)
        {
            if (thrustIn != 0)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(thrustSound);
                }
            }
            else
            {
                audioSource.Stop();
            }
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(deadSound);
            }
        }
    }

    private void ProcessParticles()
    {
        if (alive)
        {
            if (thrustIn != 0)
            {
                if (!thrustParticles.isPlaying)
                {
                    thrustParticles.Play();
                }
            }
            else
            {
                thrustParticles.Stop();
            }
        }
        else
        {
            if (thrustParticles.isPlaying)
            {
                thrustParticles.Stop();
            }

            if(!explosionParticles.isPlaying)
            {
                explosionParticles.Play();
            }
        }
    }

    private void KillPlayer()
    {
        if (!unkillable)
        {
            alive = false;

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void FreezePlayer()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public bool isAlive()
    {
        return alive;
    }
}
