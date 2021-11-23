using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landingpad : MonoBehaviour
{
    [SerializeField]
    private AudioClip successSound;

    [SerializeField]
    private ParticleSystem successParticles;

    private AudioSource audioSource;

    private bool success = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ProcessAudio();
        ProcessParticles();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Player"))
        {
            Rocket player = collisionObject.GetComponent<Rocket>();
            if (player.isAlive())
            {
                success = true;
            }
        }
    }

    private void ProcessAudio()
    {
        if (success)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(successSound);
            }
        }
    }

    private void ProcessParticles()
    {
        if (success)
        {
            if(!successParticles.isPlaying)
            {
                successParticles.Play();
            }
        }
    }

    public bool isSuccessful()
    {
        return success;
    }
}
