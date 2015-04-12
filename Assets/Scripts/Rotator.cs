using UnityEngine;
using System.Collections;

public class Rotator: MonoBehaviour
{
    public Vector3 Speed = new Vector3(0.0f, 0.0f, 0.0f);
    
    void Update()
    {
        transform.Rotate(Speed.x * Time.deltaTime, Speed.y * Time.deltaTime, Speed.z * Time.deltaTime);
    }
}
