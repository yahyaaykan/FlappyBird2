using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesisatci : MonoBehaviour
{
    private GameObject[] Holder;
    
    public float Min, Max;
    
    private float distance = 2.5f;
    
    private float LastPipeX;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        Holder = GameObject.FindGameObjectsWithTag("PipeHolder");
        for (int i = 0; i < Holder.Length; i++) {
        Vector3 temp = Holder[i].transform.position;
            temp.y = Random.Range(Min, Max);
            Holder[i].transform.position = temp;
        }
        LastPipeX = Holder[0].transform.position.x;
        for (int i = 1; i < Holder.Length; i++) {
            if (LastPipeX < Holder[i].transform.position.x)
            {
                LastPipeX=Holder[i].transform.position.x;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D hedef)
    {
        if (hedef.tag == "PipeHolder")
        {
            Vector3 temp = hedef.transform.position;
            temp.x = LastPipeX + distance;
            temp.y = Random.Range(Min, Max);
            hedef.transform.position = temp;
            LastPipeX = temp.x;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
