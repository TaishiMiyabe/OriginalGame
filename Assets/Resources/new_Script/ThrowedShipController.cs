using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowedShipController : MonoBehaviour
{
    //火と煙と爆発
    GameObject fire;
    GameObject smoke;
    public GameObject explosion;
    ParticleSystem fireParticle;
    ParticleSystem smokeParticle;

    private Rigidbody shipRigidbody;

    //飛んできた船の初期速度と初期回転速度
    private float shipVelocityX = 0;
    private float shipVelocityY;
    private float shipVelocityZ;
    private float shipRotationX = 0.15f;


    // Start is called before the first frame update
    void Start()
    {
        this.shipRigidbody = GetComponent<Rigidbody>();

        //火と煙
        fire = GameObject.Find("FlamesParticleEffect");
        smoke = GameObject.Find("SmokeEffect");
        fireParticle = fire.GetComponent<ParticleSystem>();
        smokeParticle = smoke.GetComponent<ParticleSystem>();

        //速度
        shipVelocityY = -9;
        shipVelocityZ = -110;

        //縦回転
        this.transform.Rotate(280, 0, 0);

        fireParticle.Play();
        smokeParticle.Play();




    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        this.transform.Rotate(shipRotationX, 0, 0);

        this.shipRigidbody.velocity = new Vector3(shipVelocityX, shipVelocityY, shipVelocityZ);
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Road" || other.gameObject.tag == "Player")
        {
            shipVelocityZ = 0;
            shipRotationX = 0;

            Instantiate(explosion, new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);



        }

    }
}
