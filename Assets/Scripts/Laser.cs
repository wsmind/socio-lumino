using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    public GameObject LaserPrefab;
    private GameObject m_laser;
    
    void Start()
    {
        m_laser = Instantiate(LaserPrefab, transform.position, transform.rotation) as GameObject;
    }
    
	void Update()
    {
        transform.Rotate(50.0f * Time.deltaTime, 0.0f, 0.0f);
        m_laser.transform.position = transform.position;
        m_laser.transform.rotation = transform.rotation;
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.up * 0.0f, transform.up, out hit))
        {
            m_laser.transform.localScale = new Vector3(0.1f, hit.distance, 0.1f);
        }
        else
        {
            m_laser.transform.localScale = new Vector3(5.1f, 0.1f, 5.1f);
        }
	}
}
