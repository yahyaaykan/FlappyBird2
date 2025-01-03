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
    private float Speed=3f;
    private float BounceSpeed=4f;
    private bool didFlap;
    public bool isAlive;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip flapclip,dieclip,pointclip;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        isAlive = true;
        setCameraX();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive)
        {
            Vector3 temp = transform.position;
            temp.x += Speed * Time.deltaTime;
            transform.position = temp;
            if (didFlap)
            {
                didFlap = false;
                MyRigidBody.velocity = new Vector2(0, BounceSpeed);
                audioSource.PlayOneShot(flapclip);
                Anim.SetTrigger("Flap");
            }
            if (MyRigidBody.velocity.y >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);

            }
            else {
                float angle = 0;
                angle = Mathf.Lerp(0, -90,-MyRigidBody.velocity.y / 7);
                transform.rotation=Quaternion.Euler(0,0,angle);
            }


        }
    }
    public float GetPositionX()
    {
        return transform.position.x;
    }
    void setCameraX()
    {
        CameraScripts.setX = (Camera.main.transform.position.x - transform.position.x) - 1f;
    }
    public void Uc()
    {
        didFlap=true;
    }
    void OnCollisionEnter2D(Collision2D hedef)
    {
     if (hedef.gameObject.tag=="Groung" || hedef.gameObject.tag=="Pipe")
        {
            if (isAlive)
            {
                isAlive = false;
                Anim.SetTrigger("Bluedied");
                audioSource.PlayOneShot(dieclip);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D hedef)
    {
        if (hedef.gameObject.tag == "PipeHolder" )
        {
            audioSource.PlayOneShot(pointclip);
        }
    }
}
