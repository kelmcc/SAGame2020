using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!_particleSystem.isPlaying)
            {
                _particleSystem.Play();
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_particleSystem.isPlaying)
            {
                _particleSystem.Stop();
            }
        }
    }
}
