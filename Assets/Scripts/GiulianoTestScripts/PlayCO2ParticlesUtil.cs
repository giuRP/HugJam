using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCO2ParticlesUtil : MonoBehaviour
{
    ParticleSystem co2Particles;

    private void Awake()
    {
        co2Particles = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        if(CO2Manager.Instance.co2Particles == null)
        {
            CO2Manager.Instance.co2Particles = this;
        }
    }

    public void PlayParticles()
    {
        co2Particles.Play();
    }
}
