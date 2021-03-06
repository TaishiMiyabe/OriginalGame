using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedController : MonoBehaviour
{
    private bool isDamaged = false;

    private float secondFromDamaged = 0;

    private GameObject child;

    // Start is called before the first frame update
    void Start()
    {
       child =transform.Find("Stickman_heads_sphere_child").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //ダメージを受けたとき
        if (isDamaged)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            Color color = child.GetComponent<SkinnedMeshRenderer>().material.color;
            color.a = level;
            child.GetComponent<SkinnedMeshRenderer>().material.color = color;

            secondFromDamaged += Time.deltaTime;

            if (secondFromDamaged >= 2)
            {
                isDamaged = false;
                color.a = 1;
                child.GetComponent<SkinnedMeshRenderer>().material.color = color;

                secondFromDamaged = 0;
            }
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Road")
        {
            isDamaged = true;
        }
    }
}
