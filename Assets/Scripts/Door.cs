using UnityEngine;
using System.Collections;

public class Door: MonoBehaviour
{
    public bool Active = false;
    
    public void SetActive(bool active)
    {
        Active = active;
    }
    
    void Update()
    {
        float target = 0.0f;
        if (Active)
            target = 45.0f;
        
        transform.rotation = Quaternion.Euler(target, 0.0f, 0.0f);
    }
}
