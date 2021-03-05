using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedController : MonoBehaviour
{
    private bool isDamaged = false;

    private float secondFromDamaged = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ダメージを受けたとき
        if (isDamaged)
        {
            Debug.Log("aaaaaaaa");
            //float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            float level = 0;
            Color color = gameObject.GetComponent<Renderer>().material.color;
            color.a = level;
            this.gameObject.GetComponent<Renderer>().material.color = color;

            secondFromDamaged += Time.deltaTime;

            if (secondFromDamaged >= 2)
            {
                isDamaged = false;
            }
        }
    }

    void FixedUpdate()
    {
        //ダメージを受けたとき
        if (isDamaged)
        {
            Debug.Log("aaaaaaaa");
            //float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            float level = 0;
            Color color = gameObject.GetComponent<Renderer>().material.color;
            color.a = level;
            this.gameObject.GetComponent<Renderer>().material.color = color;

            secondFromDamaged += Time.deltaTime;

            if(secondFromDamaged >= 2)
            {
                isDamaged = false;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Road")
        {
            isDamaged = true;
            Debug.Log(isDamaged);
        }
    }
}
