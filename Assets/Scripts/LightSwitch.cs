using UnityEngine;
using System.Collections;

public class LightSwitch: MonoBehaviour
{
    public GameObject[] Targets;
    public Color UnlockColor = new Color(1.0f, 0.0f, 0.0f);
    
    private bool m_active = false;
    private bool m_lightReceived = false;
    
    void Update()
    {
        if (m_lightReceived != m_active)
        {
            m_active = m_lightReceived;
            foreach (var target in Targets)
            {
                target.GetComponent<Door>().SetActive(m_active);
            }
        }
        
        m_lightReceived = false;
    }
    
    public void ReceiveLight(Color color)
    {
        if (Vector3.Dot(new Vector3(color.r, color.g, color.b), new Vector3(UnlockColor.r, UnlockColor.g, UnlockColor.b)) > 0.9)
            m_lightReceived = true;
    }
}
