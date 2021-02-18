using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interval おきにオブジェクト（プレハブ）を生成する
/// </summary>
public class ObjectGenerator : MonoBehaviour
{
    /// <summary>生成するオブジェクト（プレハブ）</summary>
    [SerializeField] GameObject m_generateObject = null;
    /// <summary>オブジェクトを生成する間隔（単位: 秒）</summary>
    [SerializeField] float m_interval = 0.25f;
    [SerializeField] Transform[] m_spawnPoints = null;
    float m_timer;

    void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_interval)
        {
            m_timer = 0;
            Instantiate(m_generateObject, m_spawnPoints[Random.Range(0, m_spawnPoints.Length)].position, m_generateObject.transform.rotation);
        }
    }
}
