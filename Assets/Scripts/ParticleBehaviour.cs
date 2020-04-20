using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{

    [SerializeField] private Vector2 m_IncubationDays;
    [SerializeField] private Vector2 m_DaysToHeal;
    [SerializeField] private Vector2 m_DaysToDie;

    [SerializeField] private float m_PercentageSymptomatic;

    [SerializeField] private float m_PercentageCritical;

    [SerializeField] private float m_PercentageCriticalsThatDie;

    private float myIncubationDays;

    private float myDaysToHeal;

    private float myDaysToDie;

    private int howManyTimesThisParticleSpreadTheInfection = 0;

    private Rigidbody2D myRigidBody;

    private SpriteRenderer mySpriteRenderer;
    private bool willDie;

    private bool isDead;

    private bool doneTransmiting = false;



    public float DayInfected { get; set; }
    // Start is called before the first frame update
    void Start()
    {

        myIncubationDays = Random.Range(m_IncubationDays.x, m_IncubationDays.y);

        myDaysToHeal = Random.Range(m_DaysToHeal.x, m_DaysToHeal.y);

        myDaysToDie = Random.Range(m_DaysToDie.x, m_DaysToDie.y);

        myRigidBody = gameObject.GetComponent<Rigidbody2D>();

        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        DecideAfterIncubationWhetherSickCriticalOrAssymptomatic();

        if ((Time.time >= DayInfected + myDaysToDie) && willDie)
        {

        }

        if (!doneTransmiting)
        {
            if (gameObject.transform.tag == "Cured" || gameObject.transform.tag == "Dead")
            {
                doneTransmiting = true;
                GameObject.Find("R0Calc").GetComponent<R0Calc>().InfectionSpreads += howManyTimesThisParticleSpreadTheInfection;
                GameObject.Find("R0Calc").GetComponent<R0Calc>().numberOfParticlesReceived += 1;
            }
        }


        CureAfterTime();

        ChangeDirectionsOnBoundaryHit();

    }

    private void DecideAfterIncubationWhetherSickCriticalOrAssymptomatic()
    {
        if (gameObject.transform.tag == "Infected")
        {
            if (Time.time >= DayInfected + myIncubationDays)
            {


                if (Random.Range(0f, 1f) < m_PercentageSymptomatic)
                {



                    if (Random.Range(0f, 1f) < m_PercentageCritical)
                    {
                        gameObject.transform.tag = "Critical";
                        mySpriteRenderer.color = Color.black;
                        gameObject.transform.SetParent(GameObject.Find("Container_ParticlesCritical").transform);




                    }
                    else
                    {
                        gameObject.transform.tag = "Sick";
                        mySpriteRenderer.color = Color.red;
                        gameObject.transform.SetParent(GameObject.Find("Container_ParticlesSick").transform);
                    }

                }
                else
                {
                    gameObject.transform.tag = "Assymptomatic";
                    mySpriteRenderer.color = Color.yellow;
                    gameObject.transform.SetParent(GameObject.Find("Container_ParticlesAssymptomatic").transform);
                }

            }
        }
    }

    private void CureAfterTime()
    {
        if (gameObject.transform.tag == "Infected" || gameObject.transform.tag == "Sick" || gameObject.transform.tag == "Assymptomatic" || gameObject.transform.tag == "Critical")
        {

            if ((Time.time >= DayInfected + myDaysToHeal) && !isDead)
            {

                gameObject.transform.tag = "Cured";
                mySpriteRenderer.color = Color.green;
                gameObject.transform.SetParent(GameObject.Find("Container_ParticlesCured").transform);
            }


        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {






        if ((gameObject.transform.tag == "Infected" || gameObject.transform.tag == "Critical" || gameObject.transform.tag == "Sick") && other.transform.tag == "NotInfected")
        {

            other.transform.tag = "Infected";
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            other.transform.SetParent(GameObject.Find("Container_ParticlesInfected").transform);
            other.gameObject.GetComponent<ParticleBehaviour>().DayInfected = Time.time;
            howManyTimesThisParticleSpreadTheInfection++;

        }



    }

    private void ChangeDirectionsOnBoundaryHit()
    {
        if ((gameObject.transform.position.x > 8.5f) && myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity = new Vector3(-myRigidBody.velocity.x, myRigidBody.velocity.y, 0);
        }
        else if ((gameObject.transform.position.x < -4.5f) && myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity = new Vector3(-myRigidBody.velocity.x, myRigidBody.velocity.y, 0);
        }
        else if (gameObject.transform.position.y > 4.8f && myRigidBody.velocity.y > 0)
        {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, -myRigidBody.velocity.y, 0);
        }
        else if (gameObject.transform.position.y < -4.8f && myRigidBody.velocity.y < 0)
        {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, -myRigidBody.velocity.y, 0);
        }
    }

}

