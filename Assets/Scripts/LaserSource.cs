using UnityEngine;
using System.Collections;

public class LaserSource: MonoBehaviour
{
    public GameObject LaserPrefab;
    public int PoolSize = 20;
    public float PhosphorescentDuration = 5.0f;
    
    public enum SourceType
    {
        AlwaysActive,
        Phosphorescent
    };
    public SourceType Type;
    
    public Color LaserColor = Color.red;
    
    private GameObject[] m_laserPool;
    private float m_phosphorescentCharge = 0.0f;
    private Material m_material;
    
    void Start()
    {
        m_material = new Material(Shader.Find("Custom/Laser"));
        m_material.color = LaserColor;
        
        m_laserPool = new GameObject[PoolSize];
        for (int i = 0; i < PoolSize; i++)
        {
            var laser = Instantiate(LaserPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<Renderer>().material = m_material;
            m_laserPool[i] = laser;
        }
        
        /*var renderer = m_laserPool[0].GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Diffuse"));
        renderer.material.color = Color.green;*/
    }
    
	void Update()
    {
        for (int i = 0; i < PoolSize; i++)
        {
            var laser = m_laserPool[i];
            laser.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        }
        
        switch (Type)
        {
            case SourceType.AlwaysActive:
            {
                // nothing to do here
                break;
            }
            
            case SourceType.Phosphorescent:
            {
                m_phosphorescentCharge -= Time.deltaTime;
                m_material.color = new Color(LaserColor.r, LaserColor.g, LaserColor.b, m_phosphorescentCharge / PhosphorescentDuration) * LaserColor.a;
                if (m_phosphorescentCharge <= 0.0f)
                    return;
                
                break;
            }
        }
        
        int laserIndex = 0;
        Vector3 direction = transform.forward;
        RaycastHit hit;
        while (laserIndex < PoolSize)
        {
            var laser = m_laserPool[laserIndex++];
            
            laser.transform.position = position;
            laser.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            
            if (Physics.Raycast(position, direction, out hit))
            {
                laser.transform.localScale = new Vector3(1.0f, hit.distance, 1.0f);
                
                var hitObject = hit.collider.gameObject;
                var source = hitObject.GetComponent<LaserSource>();
                var mirror = hitObject.GetComponent<Mirror>();
                var lightSwitch = hitObject.GetComponent<LightSwitch>();
                
                // if we found a mirror, reflect and go on
                if (mirror)
                {
                    position = hit.point;
                    direction = Vector3.Reflect(direction, hit.normal);
                    continue;
                }
                
                // in case of a phosphorescent object, we just transmit energy to this object
                // it will itself emit new rays
                if (source)
                {
                    source.ReceiveLight(LaserColor);
                }
                
                if (lightSwitch)
                {
                    lightSwitch.ReceiveLight(LaserColor);
                }
                
                break;
            }
            else
            {
                laser.transform.localScale = new Vector3(1.0f, 1000.0f, 1.0f);
                break;
            }
        }
	}
    
    void ReceiveLight(Color color)
    {
        m_phosphorescentCharge = PhosphorescentDuration;
    }
}
