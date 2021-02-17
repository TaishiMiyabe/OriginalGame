using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowedShipController : MonoBehaviour
{
    //火と煙と爆発
    GameObject fire;
    GameObject smoke;
    GameObject explosion;
    ParticleSystem fireParticle;
    ParticleSystem smokeParticle;
    ParticleSystem explosionParticle;
    private bool isExploded = false;
    private Rigidbody shipRigidbody;

    //飛んできた船の初期速度と初期回転速度
    private float shipVelocityX = 0;
    private float shipVelocityY;
    private float shipVelocityZ;
    private float shipRotationX = 0.15f;

    //船の透明度の制御のため。
    //MeshRenderer shipMR;

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
        shipVelocityY = Random.Range(-8, -10);
        shipVelocityZ = -110;

        //縦回転
        this.transform.Rotate(Random.Range(270, 280), 0, 0);

        fireParticle.Play();
        smokeParticle.Play();

        //爆発は、道と接触した時に起こす
        explosion = GameObject.Find("BigExplosionEffect");
        explosionParticle = explosion.GetComponent<ParticleSystem>();

        //船のMeshRendererを取得しておく(後で透明にするために)
        //shipMR = this.GetComponent<MeshRenderer>();

        //爆発したら、船オブジェクトを消す
        if (explosionParticle.isStopped && isExploded)
        {
            Destroy(this.gameObject);
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

            //道に接触したら爆発。(同時に船は壊れた想定のため、透明に。)
            explosionParticle.Play();

            isExploded = true;
            //shipMR.material.color = new Color(0, 0, 0, 1.0f);

        }

    }
}
