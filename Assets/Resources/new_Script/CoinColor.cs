using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CoinColor : MonoBehaviour
{
    private GameObject[] children;

    // Start is called before the first frame update
    void Start()
    {
        children = GetComponentsInChildren<Transform>().Where(com => com != this.transform).Select(x => x.gameObject).ToArray();

        foreach(var child in children)
        {
            Color color = child.GetComponent<MeshRenderer>().material.color;
            color.a = 0.7f;
            child.GetComponent<MeshRenderer>().material.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
