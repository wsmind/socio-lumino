using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour
{
    public string NextLevel;
    
    void OnTriggerEnter(Collider other)
    {
        Application.LoadLevel(NextLevel);
    }
}
