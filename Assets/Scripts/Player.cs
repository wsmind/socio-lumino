using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float MovementSpeed = 1.0f;
    public float MouseSpeed = 1.0f;
    //public float TargetSpeed = 10.0f;
    public float Gravity = 10.0f;
    public float JumpSpeed = 5.0f;
    public Camera ChildCamera;
    public GameObject SelectedBlock;
    public float SelectionScale = 0.8f;
    public float SelectionOffset = 0.2f;
    
    private CharacterController m_controller;
    private float m_verticalSpeed = 0.0f;
	
	private float m_selectedBlockRotation = 0.0f;
	
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(ChildCamera.transform.position, ChildCamera.transform.forward, out hit))
        {
            var hitObject = hit.collider.gameObject;
            if (!SelectedBlock)
            {
                var block = hitObject.GetComponent<MoveableBlock>();
                
                if (block)
                {
                    // TODO: draw hovered block
                    
                    if (Input.GetButtonDown("Fire1"))
                    {
                        // grab the block
                        SelectedBlock = hitObject;
                        SelectedBlock.transform.localScale = new Vector3(SelectionScale, SelectionScale, SelectionScale);
                        hit.collider.enabled = false;
                    }
                }
            }
            else
            {
                var placeable = hitObject.GetComponent<PlaceableBlock>();
                if (placeable)
                {
                    // move the selected block with the camera
                    SelectedBlock.transform.position = hitObject.transform.position + hitObject.transform.forward * SelectionOffset;
                    SelectedBlock.transform.rotation = hitObject.transform.rotation;
					SelectedBlock.transform.Rotate(0.0f, 0f, m_selectedBlockRotation);
                    
                    // stick the block to a placeable
                    if (Input.GetButtonDown("Fire1"))
                    {
                        //var block = SelectedBlock.GetComponent<MoveableBlock>();
                        SelectedBlock.transform.position = hitObject.transform.position;
                        SelectedBlock.transform.rotation = hitObject.transform.rotation;
						SelectedBlock.transform.Rotate(0.0f, 0f, m_selectedBlockRotation);
                        SelectedBlock.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        SelectedBlock.GetComponent<Collider>().enabled = true;
                        SelectedBlock = null;
                    }
					if (Input.GetButtonDown("Fire2"))
					{
						m_selectedBlockRotation += 90f;
					}
                }
            }
        }
    }
    
	void FixedUpdate()
    {
        Vector2 mouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        transform.Rotate(0.0f, mouse.x * MouseSpeed, 0.0f);
        ChildCamera.transform.Rotate(-mouse.y * MouseSpeed, 0.0f, 0.0f);
        
        if (m_controller.isGrounded)
        {
            m_verticalSpeed = -1.0f;
            
            if (Input.GetButtonDown("Jump"))
            {
                m_verticalSpeed = JumpSpeed;
            }
        }
        else
        {
            m_verticalSpeed -= Gravity * Time.deltaTime;
        }
        
        Vector3 velocity = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical")) * MovementSpeed;
        velocity.y = m_verticalSpeed * Time.deltaTime;
        //transform.position += movement * MovementSpeed;
        m_controller.Move(velocity);
        //if (m_rigidBody.velocity.sqrMagnitude < TargetSpeed * TargetSpeed)
        //    m_rigidBody.AddForce(movement * MovementSpeed, ForceMode.Impulse);
	}
}
