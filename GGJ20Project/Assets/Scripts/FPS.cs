using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPS : MonoBehaviour
{
    private float speed = - 5.0f;
    private float m_MovX;
    private float m_MovY;
    private Vector3 m_moveHorizontal;
    private Vector3 m_movVertical;
    private Vector3 m_velocity;
    private Rigidbody m_Rigid;
    private float m_yRot;
    private float m_xRot;
    private Vector3 m_rotation;
    private Vector3 m_cameraRotation;
    private float m_lookSensitivity = 3.0f;
    private bool m_cursorIsLocked = true;

    public Camera m_Camera;

    private Animator m_Animator;

    private void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
    }

    public void Update()
    {

        m_MovX = Input.GetAxis("Horizontal");
        m_MovY = Input.GetAxis("Vertical");

        m_moveHorizontal = transform.right * - m_MovX;
        m_movVertical = transform.forward * - m_MovY;

        m_velocity = (m_moveHorizontal + m_movVertical).normalized * speed;

       // m_yRot = Input.GetAxisRaw("Mouse X");
        //m_rotation = new Vector3(0, m_yRot, 0) * m_lookSensitivity;

        //m_xRot = Input.GetAxisRaw("Mouse Y");
        //m_cameraRotation = new Vector3(m_xRot, 0, 0) * m_lookSensitivity;


       m_yRot = Mathf.Min(60, Mathf.Max(-60, m_yRot + Input.GetAxis("Mouse Y")));
       m_xRot += Input.GetAxis("Mouse X");
       transform.localRotation = Quaternion.Euler(0, m_xRot, 0);
       m_Camera.transform.localRotation = Quaternion.Euler(-m_yRot, 0, 0);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = - 7.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = - 5.0f;
        }

        if(Input.GetKeyDown(KeyCode.LeftControl)){
            m_Camera.fieldOfView = 53.0f;
        }

        if(Input.GetKeyUp(KeyCode.LeftControl)){
            m_Camera.fieldOfView = 69.0f;
        }
        if (m_velocity != Vector3.zero)
        {
            m_Rigid.MovePosition(m_Rigid.position + m_velocity * Time.fixedDeltaTime);
        }

        if (Input.GetMouseButtonDown(0) && m_Animator != null )
        {
            m_Animator.SetTrigger("active");
        }

        if (m_rotation != Vector3.zero)
        {
            //rotate the camera of the player
            
            //m_Rigid.MoveRotation(m_Rigid.rotation * Quaternion.Euler(m_rotation));            
        }

        if (m_Camera != null)
        {
            //negate this value so it rotates like a FPS not like a plane
            //m_Camera.transform.Rotate(-m_cameraRotation);
        }

        InternalLockUpdate();

    }

    //controls the locking and unlocking of the mouse
    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            UnlockCursor();
        }
        else if (!m_cursorIsLocked)
        {
            LockCursor();
        }
    }


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("interactable")) {
            m_Animator = other.gameObject.GetComponent<Animator>();
        }
    }
    private void OnTriggerExit(Collider other) {
        m_Animator = null;
    }
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}