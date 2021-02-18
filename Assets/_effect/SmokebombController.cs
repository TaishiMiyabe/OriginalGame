using UnityEngine;

public class SmokebombController : MonoBehaviour
{
    [SerializeField] GameObject m_destoyEffect = null;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(m_destoyEffect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
