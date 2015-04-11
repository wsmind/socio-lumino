using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    public float MovementSpeed = 1.0f;
    public float MouseSpeed = 1.0f;
    public float TargetSpeed = 10.0f;
    public Camera ChildCamera;
    
    private Rigidbody m_rigidBody;
    
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }
    
	void FixedUpdate()
    {
        Vector2 mouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        transform.Rotate(0.0f, mouse.x * MouseSpeed, 0.0f);
        ChildCamera.transform.Rotate(-mouse.y * MouseSpeed, 0.0f, 0.0f);
        
        Vector3 movement = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        //transform.position += movement * MovementSpeed;
        if (m_rigidBody.velocity.sqrMagnitude < TargetSpeed * TargetSpeed)
            m_rigidBody.AddForce(movement * MovementSpeed, ForceMode.Impulse);
	}
}
