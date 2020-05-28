using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovemtPlayer : MonoBehaviour
{

    public float m_playerSpeed = 5f;
    public float m_sprintSpeed = 1f;

    private CharacterController m_charControll;
    private readonly float m_jumpPower = 3f;
    private float m_ySpeed = 0;
    private Vector3 move = Vector3.zero;

    private void Awake()
    {
        m_charControll = GetComponent<CharacterController>();
    }

    void Update()
    {
        ProcessManualMovement(); // Movment
    }

    private void ProcessManualMovement()
    {
        bool Jump = Input.GetButtonDown("Jump");
        bool Sprint = Input.GetButton("Sprint");
        move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        move = move.normalized;
        if (m_charControll.isGrounded) //Gravitation
        {
            if (Jump)
            {
                m_ySpeed = m_jumpPower;
            }
            else
            {
                m_ySpeed = 0;
            }
        }
        else
        {
            m_ySpeed -= Physics.gravity.y * Time.deltaTime;
        }

        move.y = m_ySpeed;
        move = transform.TransformDirection(move);

        m_sprintSpeed = m_playerSpeed; //Sprinten und Sneeken

        if (Sprint && m_charControll.isGrounded)
        {
            m_sprintSpeed = m_playerSpeed * 2f;
        }

        m_charControll.Move(move * m_sprintSpeed * Time.deltaTime); //Move mit vor eingestelltem Move-Speed (m_sprintSpeed)
    }
}
