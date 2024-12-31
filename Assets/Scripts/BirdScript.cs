using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public static BirdScript instance;
    [SerializeField]
    private Rigidbody2D MyRigidBody;
    [SerializeField]
    private Animator Anim;
    private float Speed;
    private float BounceSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
