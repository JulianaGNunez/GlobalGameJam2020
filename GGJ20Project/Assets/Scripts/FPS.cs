using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPS : MonoBehaviour
{
    private float speed = -5.0f;
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

    private ModeManager m_ModeManager;

    [Header("Recursos")]
     int rec_madeiras = 1;
     int rec_fitas = 2;
     int rec_registros = 1;
    public TextMeshProUGUI txt_madeiras_qtd;
    public TextMeshProUGUI txt_fitas_qtd;
    public TextMeshProUGUI txt_registros_qtd;


    private void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
        m_ModeManager = GetComponent<ModeManager>();
        attRecursos(rec_madeiras,"madeiras");
        attRecursos(rec_fitas, "fitas");
        attRecursos(rec_registros, "registros");
    }

    public void Update()
    {

        m_MovX = Input.GetAxis("Horizontal");
        m_MovY = Input.GetAxis("Vertical");

        m_moveHorizontal = transform.right * -m_MovX;
        m_movVertical = transform.forward * -m_MovY;

        m_velocity = (m_moveHorizontal + m_movVertical).normalized * speed;

        // m_yRot = Input.GetAxisRaw("Mouse X");
        //m_rotation = new Vector3(0, m_yRot, 0) * m_lookSensitivity;

        //m_xRot = Input.GetAxisRaw("Mouse Y");
        //m_cameraRotation = new Vector3(m_xRot, 0, 0) * m_lookSensitivity;


        m_yRot = Mathf.Min(60, Mathf.Max(-60, m_yRot + Input.GetAxis("Mouse Y")));
        m_xRot += Input.GetAxis("Mouse X");
        transform.localRotation = Quaternion.Euler(0, m_xRot, 0);
        m_Camera.transform.localRotation = Quaternion.Euler(-m_yRot, 0, 0);

        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    speed = -7.5f;
        //}
        //if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    speed = -5.0f;
        //}

        //if (Input.GetKeyDown(KeyCode.LeftControl))
        //{
        //    m_Camera.fieldOfView = 53.0f;
        //}

        //if (Input.GetKeyUp(KeyCode.LeftControl))
        //{
        //    m_Camera.fieldOfView = 69.0f;
        //}
        if (m_velocity != Vector3.zero)
        {
            m_Rigid.MovePosition(m_Rigid.position + m_velocity * Time.fixedDeltaTime);
        }

        if (Input.GetButtonDown("Jump") && m_Animator != null)
        {
            m_Animator.SetBool("active", true);
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
        //if (Input.GetKeyUp(KeyCode.Escape))
        //{
        //    m_cursorIsLocked = false;
        //}
        //else if (Input.GetButtonDown("Jump"))
        //{
        //    m_cursorIsLocked = true;
        //}

        if (m_cursorIsLocked)
        {
            UnlockCursor();
        }
        else if (!m_cursorIsLocked)
        {
            LockCursor();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("interactable"))
        {
            var interactable = other.gameObject.GetComponent<interactableType>();
            switch (interactable.tipo)
            {
                case interactableType.interacType.pipes:
                    if (rec_fitas > 0 && interactable.isBroken)
                    {
                        interactable.isBroken = false;
                        attRecursos(--rec_fitas, "fitas");
                        m_Animator = other.gameObject.GetComponent<Animator>();
                    }
                    break;
                case interactableType.interacType.wall:
                    if (rec_madeiras > 0 && interactable.isBroken)
                    {
                        interactable.isBroken = false;
                        attRecursos(--rec_madeiras, "madeiras");
                        m_Animator = other.gameObject.GetComponent<Animator>();
                    }
                    break;
                case interactableType.interacType.valve:
                    if (rec_registros > 0 && interactable.isBroken)
                    {
                        interactable.isBroken = false;
                        attRecursos(--rec_registros, "registros");
                        m_Animator = other.gameObject.GetComponent<Animator>();
                    }
                    break;
                default:
                    break;
            }
        }
        if (other.gameObject.CompareTag("collectable"))
        {
            if (other.gameObject.name.Contains("madeira"))
            {
                attRecursos(++rec_madeiras, "madeiras");
            }
            else if (other.gameObject.name.Contains("fita"))
            {
                attRecursos(++rec_fitas, "fitas");
            }
            else if (other.gameObject.name.Contains("registro"))
            {
                attRecursos(++rec_registros, "registros");
            }
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        m_Animator = null;
    }
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void attRecursos(int newValue, string type)
    {
        switch (type)
        {
            case "madeiras":
                rec_madeiras = newValue;
                txt_madeiras_qtd.text = "<size=50%>x<size=100%>" + rec_madeiras;
                break;
            case "fitas":
                rec_fitas = newValue;
                txt_fitas_qtd.text = "<size=50%>x<size=100%>" + rec_fitas;
                break;
            case "registros":
                rec_registros = newValue;
                txt_registros_qtd.text = "<size=50%>x<size=100%>" + rec_registros;
                break;
            default:
                break;
        }
    }


}