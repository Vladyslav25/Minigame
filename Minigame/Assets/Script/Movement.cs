using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float m_gameSpeed = 5f;
    public float m_sprintSpeed = 1f;
    public float m_stamina;
    public Text ui_staminaText;
    public Text ui_manaText;
    public RectTransform m_staminaBar;
    public float m_staminaRegTimer = 0;
    public int m_ManaKristal = 0;

    private LayerMask hitLayers = ~0;
    private bool isSneeking = false;
    private CamMovmentwithPos script_CamMove;
    private CharacterController m_charControll;
    private readonly float m_jumpPower = 3f;
    private readonly float m_gravity = 9.81f;
    private float m_ySpeed = 0;
    private Vector3 move = Vector3.zero;
    private Vector3 mouse;
    private Ray castPoint;
    private LevelChanger script_LevelChanger;
    private SaveLoadLv1 script_SaveLoadLv1 = null;
    private SaveLoadLv2 script_SaveLoadLv2 = null;

    public int ManaKristal
    {
        get
        {
            return m_ManaKristal;
        }
        set
        {
            GiveMana(value);
        }
    }

    public float StaminaRegTimer //Ein Timer nachdem die AUsdauer sich wieder regenerieren kann
    {
        get { return m_staminaRegTimer; }
        private set
        {
            m_staminaRegTimer = Mathf.Clamp(value, 0, 1.5f);
        }
    }

    public float Stamina
    {
        get { return m_stamina; }
        set
        {
            m_stamina = Mathf.Clamp(value, 0, 100);

            m_staminaBar.localScale = new Vector3(m_stamina / 100, 1, 1);
            ui_staminaText.text = Mathf.RoundToInt(m_stamina).ToString();
        }
    }




    private void Awake()
    {
        Stamina = 100;
        script_CamMove = FindObjectOfType<CamMovmentwithPos>();
        m_charControll = GetComponent<CharacterController>();
        script_LevelChanger = FindObjectOfType<LevelChanger>();
        
    }

    void Update()
    {
        switch (script_LevelChanger.m_currentLevel)
        {
            case 1:
                script_SaveLoadLv1 = FindObjectOfType<SaveLoadLv1>();
                break;
            case 2:
                script_SaveLoadLv2 = FindObjectOfType<SaveLoadLv2>();
                break;

        }
        ProcessManualMovement(); // Movment
        ui_manaText.text = $"Mana: {m_ManaKristal}";
        if (Input.GetButton("HM"))
        {
            if (script_LevelChanger.m_currentLevel == 1)
            {
                script_SaveLoadLv1.Save();
            }
            if (script_LevelChanger.m_currentLevel == 2)
            {
                script_SaveLoadLv2.Save();
            }
            script_LevelChanger.HM();
        }
    }

    private void GiveMana(int _newAmount)
    {
        m_ManaKristal = _newAmount;
        ui_manaText.text = $"Mana: {m_ManaKristal}";

    }

    private void ProcessManualMovement()
    {
        bool Jump = Input.GetButtonDown("Jump");
        bool Sprint = Input.GetButton("Sprint");
        bool Sneak = Input.GetButton("Sneak");

        if (script_CamMove.m_pointerIndex == 0 && !(script_CamMove.m_inMovment)) // Wenn man von oben guckt
        {
            mouse = Input.mousePosition;                        //holt sich die MausPos
            castPoint = Camera.main.ScreenPointToRay(mouse);    //Macht einen ray von der MausPos
            if (Physics.Raycast(castPoint, out RaycastHit hit, Mathf.Infinity, hitLayers)) //Wenn hit
            {
                Vector3 lookAt = hit.point;
                lookAt.y = transform.position.y; // hebt den Effect auf, dass der Hit.point nicht auf Bodenhöhe ist, sondern auf Spielerhöhe
                                                 // ist dazu da damit sich beim LookAt der Spieler keine Rotationen richtung Boden hat
                transform.LookAt(lookAt);
            }
        }
        //normale TastenMoves sind von CamView unabhängig
        move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        move = move.normalized;
        if (m_charControll.isGrounded) //Gravitation
        {
            if (Jump)
            {
                StaminaRegTimer = 1.5f; //Beim versuch zu Springen soll der Timer von neu beginnen
                if (Stamina > 5)    //Nur wenn genug Ausdauer da ist soll man springen
                {
                    m_ySpeed = m_jumpPower;
                    Stamina -= 5;
                }
            }
            else
            {
                m_ySpeed = 0;
            }
        }
        else
        {
            m_ySpeed -= m_gravity * Time.deltaTime;
        }

        move.y = m_ySpeed;
        move = transform.TransformDirection(move);

        m_sprintSpeed = m_gameSpeed; //Sprinten und Sneeken

        if (Sprint && m_charControll.isGrounded)
        {
            StaminaRegTimer = 1.5f;
            if (Stamina >= 1)
            {
                Stamina -= 1;
                m_sprintSpeed = m_gameSpeed * 2f;
            }
        }
        else
        {
            if (Sneak)
            {
                m_sprintSpeed = m_gameSpeed * 0.5f;
                isSneeking = true;
            }

            if (StaminaRegTimer != 0)
            {
                StaminaRegTimer -= 1 * Time.deltaTime;
            }

            if (StaminaRegTimer == 0) //Reg der Ausdauer wenn Timer 0 erreicht
            {
                if (!isSneeking) //beim Sneeken soll man schneller Ausdauer regen
                {
                    Stamina += 1;
                }
                else
                {
                    Stamina += 1.5f;
                }
            }

        }
        m_charControll.Move(move * m_sprintSpeed * Time.deltaTime); //Move mit vor eingestelltem Move-Speed (m_sprintSpeed)
    }
}
