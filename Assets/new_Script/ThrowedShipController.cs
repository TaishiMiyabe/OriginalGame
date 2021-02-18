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
    //ParticleSystem explosionParticle;
    private bool isExploded = false;
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

        //爆発は、道と接触した時に起こす
        //explosion = GameObject.Find("BigExplosionEffect");
        //explosionParticle = explosion.GetComponent<ParticleSystem>();


        //爆発したら、爆発を消す
        if (isExploded)
        {
            Destroy(explosion);
        }

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(shipRotationX, 0, 0);

        this.shipRigidbody.velocity = new Vector3(shipVelocityX, shipVelocityY, shipVelocityZ);

        
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Road")
        {
            shipVelocityZ = 0;
            shipRotationX = 0;

            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);


            isExploded = true;

        }

    }
}
