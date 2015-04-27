using UnityEngine;
using System.Collections;

public class Plateforme : MonoBehaviour {
	
	public float PositionX = 0f;
	public float PositionY = 0f;
    public float PositionZ = 0f;
	
	public float DeplacementX = 0f;
	public float DeplacementY = 0f;
    public float DeplacementZ = 0f;
	public bool Active = false;
	public int NbreSwitch = 1;
	public float Speed = 0.1f;
	
	public enum Deplacement
    {
        Simple,
        AllerRetour
    };
    public Deplacement Cas;
	
	
	private Vector3 m_currentPosition = new Vector3(0.0f,0.0f,0.0f);
    private int NbreSwitchActif = 0;
	private bool Aller = true;

	// Use this for initialization
	void Start () {
		m_currentPosition = new Vector3(PositionX,PositionY,PositionZ);
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
	
	// Update is called once per frame
	 void Update()
    {
		float targetX = PositionX;
		float targetY = PositionY;
		float targetZ = PositionZ;
		
        if (Active)
		{
			if (Cas == Deplacement.AllerRetour) 
			{
				if (Aller)
				{
					targetX = DeplacementX;
					targetY = DeplacementY;
					targetZ = DeplacementZ;
				}
				if (m_currentPosition == new Vector3(targetX,targetY,targetZ)) Aller = !(Aller);
			}
			else
			{
				targetX = DeplacementX;
				targetY = DeplacementY;
				targetZ = DeplacementZ;
			}
		}
		m_currentPosition[0] += (targetX - m_currentPosition[0]) * Speed ;
        m_currentPosition[1] += (targetY - m_currentPosition[1]) * Speed;
        m_currentPosition[2] += (targetZ - m_currentPosition[2]) * Speed;
        
        transform.position = m_currentPosition;
    }
}

