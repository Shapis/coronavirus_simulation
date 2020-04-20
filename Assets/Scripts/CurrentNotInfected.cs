using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentNotInfected : MonoBehaviour
{
    [SerializeField] GameObject[] Containers;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int temp = 0;
        foreach (var c in Containers)
        {
            temp += c.transform.childCount;
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = temp.ToString();
    }
}
