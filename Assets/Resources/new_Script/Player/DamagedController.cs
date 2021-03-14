using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedController : MonoBehaviour
{
    private bool isDamaged = false;

    private float secondFromDamaged = 0;

    private GameObject child;

    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
       child =transform.Find("Stickman_heads_sphere_child").gameObject;

        this.playerAnimator = GetComponent<Animator>();
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

            this.playerAnimator.SetFloat("RunSpeed", 0.25f);

            if (secondFromDamaged >= 2)
            {
                isDamaged = false;
                color.a = 1;
                child.GetComponent<SkinnedMeshRenderer>().material.color = color;

                this.playerAnimator.SetFloat("RunSpeed", 1f);

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
