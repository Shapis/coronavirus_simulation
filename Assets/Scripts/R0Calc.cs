using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R0Calc : MonoBehaviour
{

    public float InfectionSpreads { get; set; }

    public float numberOfParticlesReceived { get; set; }

    public float MyR0 { get; set; }

    GameObject ParticleGenerator;

    private void Awake()
    {
        numberOfParticlesReceived = 0;
        InfectionSpreads = 0;

        Time.timeScale = 2f;
    }
    // Start is called before the first frame update
    void Start()
    {
        ParticleGenerator = GameObject.Find("ParticleGenerator");
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfParticlesReceived < ParticleGenerator.GetComponent<ParticleGenerator>().m_NumberOfParticles / 8)
        {
            MyR0 = InfectionSpreads / numberOfParticlesReceived;
        }

    }
}
