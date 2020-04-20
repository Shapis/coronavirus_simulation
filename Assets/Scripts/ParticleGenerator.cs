using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour
{


    [SerializeField] private GameObject m_ParticlesContainerNotInfected;

    [SerializeField] private GameObject m_ParticlesContainerInfected;

    [SerializeField] private GameObject m_ParticlesContainerQuarantine;

    [SerializeField] private GameObject m_ParticlePrefab;











    [SerializeField] private float m_PercentageQuarantined;
    [SerializeField] public int m_NumberOfParticles;

    [SerializeField] private int m_NumberOfParticlesInfected;

    [SerializeField] private float m_ParticleMaxVelocity;


    // Start is called before the first frame update
    void Start()
    {




        for (int i = 0; i < m_NumberOfParticles - m_NumberOfParticlesInfected; i++)
        {
            GenerateParticle("NotInfected");
        }

        for (int i = 0; i < m_NumberOfParticlesInfected; i++)
        {
            GenerateParticle("Infected");
        }
    }



    private void GenerateParticle(string myTag)
    {
        GameObject myParticle = Instantiate(m_ParticlePrefab, new Vector3(Random.Range(-4.5f, 8.8f), Random.Range(-4.35f, 4.35f), 0), Quaternion.identity);
        myParticle.transform.SetParent(m_ParticlesContainerNotInfected.transform);




        myParticle.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-m_ParticleMaxVelocity, m_ParticleMaxVelocity), Random.Range(-m_ParticleMaxVelocity, m_ParticleMaxVelocity), 0);

        myParticle.transform.tag = myTag;

        if (Random.Range(0f, 1f) < m_PercentageQuarantined)
        {
            if (myTag != "Infected")
            {
                myParticle.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }



        }

        if (myTag == "NotInfected")
        {
            myParticle.GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (myTag == "Infected")
        {
            myParticle.GetComponent<SpriteRenderer>().color = Color.yellow;

        }

    }
}
