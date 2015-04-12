using UnityEngine;
using System.Collections;

public class Door: MonoBehaviour
{
    public bool Active = false;
    public float YRotation = 180.0f;
    
    private float m_currentRotation = 0.0f;
    
    void Start()
    {
        m_currentRotation = YRotation;
    }
    
    public void SetActive(bool active)
    {
        Active = active;
    }
    
    void Update()
    {
        float target = YRotation;
        if (Active)
            target = YRotation + 90.0f;
        
        m_currentRotation += (target - m_currentRotation) * Time.deltaTime;
        
        transform.rotation = Quaternion.Euler(0.0f, m_currentRotation, 0.0f);
    }
}
