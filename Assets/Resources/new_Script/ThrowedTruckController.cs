using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowedTruckController : MonoBehaviour, IPointerClickHandler
{
    GameObject fire;
    GameObject smoke;
    public GameObject explosion;

    ParticleSystem fireParticle;
    ParticleSystem smokeParticle;

    private Rigidbody smallTruckRigidbody;

    private float truckVelocity_x;
    private float truckVelocity_y;
    private float truckVelocity_z;

    // Start is called before the first frame update
    void Start()
    {
        this.smallTruckRigidbody = GetComponent<Rigidbody>();

        //火と煙
        fire = GameObject.Find("FlamesParticleEffect");
        smoke = GameObject.Find("SmokeEffect");
        fireParticle = fire.GetComponent<ParticleSystem>();
        smokeParticle = smoke.GetComponent<ParticleSystem>();

        truckVelocity_x = 0;
        truckVelocity_y = 0;
        truckVelocity_z = Random.Range(-80,-100);

        //初期で少し傾きをつける　ここ多分変える。
        this.transform.Rotate(Random.Range(0,20), Random.Range(70, 110), Random.Range(-20,-40));

        fireParticle.Play();
        smokeParticle.Play();

        this.smallTruckRigidbody.velocity = new Vector3(truckVelocity_x, truckVelocity_y, truckVelocity_z);
    }

    // Update is called once per frame
    void Update()
    {
       // this.smallTruckRigidbody.velocity = new Vector3(truckVelocity_x, truckVelocity_y, truckVelocity_z);
    }

    public void OnClickAct()
    {
        Debug.Log("OnClickAct");
        Instantiate(explosion, new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z), Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");
    }
}
