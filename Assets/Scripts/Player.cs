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
    public GameObject SelectedBlock;
    public float SelectionScale = 0.8f;
    public float SelectionOffset = 0.2f;
    
    private Rigidbody m_rigidBody;
    
    void Start()
    {
        Cursor.visible = false;
        m_rigidBody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(ChildCamera.transform.position, ChildCamera.transform.forward, out hit))
        {
            if (!SelectedBlock)
            {
                // TODO: draw hovered block
                
                // try to grab a block
                if (Input.GetButtonDown("Fire1"))
                {
                    SelectedBlock = hit.collider.gameObject;
                    SelectedBlock.transform.localScale = new Vector3(SelectionScale, SelectionScale, SelectionScale);
                    hit.collider.enabled = false;
                }
            }
            else
            {
                // move the selected block with the camera
                var target = hit.collider.gameObject;
                SelectedBlock.transform.position = target.transform.position + target.transform.up * SelectionOffset;
                SelectedBlock.transform.rotation = target.transform.rotation;
                
                // try to stick a block to a wall
                if (Input.GetButtonDown("Fire1"))
                {
                    SelectedBlock.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    SelectedBlock.GetComponent<Collider>().enabled = true;
                    SelectedBlock = null;
                }
            }
        }
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
