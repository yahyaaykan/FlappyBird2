using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temizlikci : MonoBehaviour
{
    private GameObject[] Background;
    private GameObject[] Ground;
    private float LastBGX;
    private float LastGroundX;
    void Awake()
    {
        Background = GameObject.FindGameObjectsWithTag("Background");
        Ground = GameObject.FindGameObjectsWithTag("Groung");
        LastBGX = Background[0].transform.position.x;
        LastGroundX = Ground[0].transform.position.x;
        for (int i = 1; i < Background.Length; i++)
        {
            if (LastBGX < Background[i].transform.position.x)
            {
                LastBGX = Background[i].transform.position.x;
            }
        }
        for (int i = 1; i < Ground.Length; i++)
        {
            if (LastGroundX < Ground[i].transform.position.x)
            {
                LastGroundX = Ground[i].transform.position.x;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D hedef)
    {
        if (hedef.tag == "Background") { 
        Vector3 temp = hedef.transform.position;
            float genislik = ((BoxCollider2D)hedef).size.x;
            temp.x = genislik+LastBGX;
            hedef.transform.position = temp;
            LastBGX = temp.x;
        }
        if (hedef.tag == "Groung")
        {
            Vector3 temp = hedef.transform.position;
            float genislik = ((BoxCollider2D)hedef).size.x;
            temp.x = genislik + LastGroundX;
            hedef.transform.position = temp;
            LastGroundX = temp.x;

        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
