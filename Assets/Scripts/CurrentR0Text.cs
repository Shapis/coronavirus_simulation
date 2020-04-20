using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CurrentR0Text : MonoBehaviour
{

    [SerializeField] GameObject R0Calc;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = String.Format("{0:.##}", R0Calc.GetComponent<R0Calc>().MyR0);


    }
}
