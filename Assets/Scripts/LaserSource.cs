using UnityEngine;
using System.Collections;

public class LaserSource: MonoBehaviour
{
    public GameObject LaserPrefab;
    public int PoolSize = 20;
    public bool Active = true;
    
    private GameObject[] m_laserPool;
    
    void Start()
    {
        m_laserPool = new GameObject[PoolSize];
        for (int i = 0; i < PoolSize; i++)
        {
            var laser = Instantiate(LaserPrefab, transform.position, transform.rotation) as GameObject;
            m_laserPool[i] = laser;
        }
    }
    
	void Update()
    {
        transform.Rotate(50.0f * Time.deltaTime, 0.0f, 0.0f);
        
        for (int i = 0; i < PoolSize; i++)
        {
            var laser = m_laserPool[i];
            laser.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        }
        
        if (!Active)
            return;
        
        int laserIndex = 0;
        Vector3 position = transform.position;
        Vector3 direction = transform.up;
        RaycastHit hit;
        while (laserIndex < PoolSize)
        {
            var laser = m_laserPool[laserIndex];
            
            laser.transform.position = position;
            laser.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            
            if (Physics.Raycast(position, direction, out hit))
            {
                laser.transform.localScale = new Vector3(1.0f, hit.distance, 1.0f);
                
                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
            }
            else
            {
                laser.transform.localScale = new Vector3(1.0f, 1000.0f, 1.0f);
                break;
            }
            
            laserIndex++;
        }
	}
}
