using UnityEngine;
using System.Collections;

public class Door: MonoBehaviour
{
    public bool Active = false;
	public int NbreSwitch = 1;
    public float YRotation = 180.0f;
    
    private float m_currentRotation = 0.0f;
	private int NbreSwitchActif = 0;
    
    void Start()
    {
        m_currentRotation = YRotation;
    }
    
    public void SetActive(bool active)
    {
		if (active)
		{
			NbreSwitchActif += 1; 
		}
		else
		{
			NbreSwitchActif -= 1; 
		}
		if (NbreSwitchActif == NbreSwitch)
			Active = true;
		else
			Active = false;
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
