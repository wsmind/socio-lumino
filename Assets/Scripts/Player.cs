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
        Cursor.lockState = CursorLockMode.Locked;
        m_rigidBody = GetComponent<Rigidbody>();
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
                    SelectedBlock.transform.position = hitObject.transform.position + hitObject.transform.up * SelectionOffset;
                    SelectedBlock.transform.rotation = hitObject.transform.rotation;
                    
                    // stick the block to a placeable
                    if (Input.GetButtonDown("Fire1"))
                    {
                        var block = SelectedBlock.GetComponent<MoveableBlock>();
                        SelectedBlock.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        SelectedBlock.GetComponent<Collider>().enabled = true;
                        SelectedBlock = null;
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
        
        Vector3 movement = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        //transform.position += movement * MovementSpeed;
        if (m_rigidBody.velocity.sqrMagnitude < TargetSpeed * TargetSpeed)
            m_rigidBody.AddForce(movement * MovementSpeed, ForceMode.Impulse);
	}
}
