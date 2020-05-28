using UnityEngine;
using UnityEngine.UI;

public class CamMovmentwithPos : MonoBehaviour
{
    //public 
    public Transform m_camGameObjectPLAYER;     // Pos for the PlayerCam Pos
    public Transform m_camGameObjectUP;         // Pos for the UpCam Pos
    public Transform m_player;                  // Player
    public float m_rotationSpeedX = 120f;       // rotation Speed
    public float m_rotationSpeedY = 120f;       // rotation Speed
    public float m_CamMoveSpeed = 30f;          // Cam Switch Speed
    public bool m_inMovment = false;            // while in CamMovment
    public Text ui_currentView;
    public Text ui_currentMana;
    public RectTransform ui_ManaBar;
    public Text ui_missingManaMess;
    public int m_pointerIndex = 1;              // Pointer for the Array | Needs in Movment

    //private
    private float m_rotX = 0;                    // for mouse rotX
    private Vector3 Vel = Vector3.zero;
    private Transform[] m_CamPoses;         // Array of possible CamPoses
    private Movement script_playerMovemnt;
    private float m_ManaTimer = 3f;
    private Timer script_manaTimer;
    private bool m_missingManaOn = false;
    private float m_missingManaCounter = 3f;

    public float ManaTime
    {
        get
        {
            return m_ManaTimer;
        }
        set
        {
            m_ManaTimer = Mathf.Clamp(value, 0f, 3f);
        }
    }

    private void Awake()
    {
        script_playerMovemnt = FindObjectOfType<Movement>();
        script_manaTimer = FindObjectOfType<Timer>();
        ui_missingManaMess.gameObject.SetActive(false);
    }
    void Start()
    {
        m_CamPoses = new Transform[] { m_camGameObjectUP, m_camGameObjectPLAYER }; //make an Array to switch between the two camPoses
    }

    //Used FixedUpdate: In Update the Camera vibrates 
    private void FixedUpdate()
    {
        if (m_missingManaOn)
        {
            m_missingManaCounter -= Time.deltaTime * 1f;
            if (Mathf.RoundToInt(m_missingManaCounter) == 0)
            {
                ui_missingManaMess.gameObject.SetActive(false);
                m_missingManaCounter = 3f;
            }
        }
        if (m_inMovment)
        {
            ChangeView();
        }

        if (ManaTime == 0 && !m_inMovment && m_pointerIndex == 0)
        {
            ChangePointer();
            m_inMovment = true;
            ManaTime = 3f;
        }

        if (Input.GetButtonUp("CamChange") && !m_inMovment)
        {
            if (true) //change the Pointer at m_CamPoses
            {
                ChangePointer();
                m_inMovment = true;
            }
        }
    }
    private void Update()
    {
       
        if (m_pointerIndex == 1) //Player
        {
            script_manaTimer.Hide();
            if (!m_inMovment)
            {
                ProcessRotation(); //Casual Player Rotation |  edit: Advanced Player Rotation
            }
            ui_currentView.text = "Current View: Player";
        }
        else // Up
        {
            if (!m_inMovment)
            {
                transform.rotation = Quaternion.Euler(90f, 0f, 0f); //rotate the Cam down when the Move is fin
            }
            script_manaTimer.Show();
            ui_currentView.text = "Current View: Up";
            ManaTime -= 0.5f * Time.deltaTime;
            ui_currentMana.text = Mathf.RoundToInt(ManaTime).ToString();
            ui_ManaBar.localScale = new Vector3(ManaTime / 3, 1, 1);
        }

    }

    private void ChangePointer() //switch the Poniter between the two possible Poses
    {
        if (m_pointerIndex == 1 && script_playerMovemnt.ManaKristal > 0)
        {
            m_pointerIndex--;
            script_playerMovemnt.m_ManaKristal--;
            ManaTime = 3f;
            return;
        }
        else
        {
            if (script_playerMovemnt.ManaKristal == 0 && m_pointerIndex == 1)
            {
                ui_missingManaMess.gameObject.SetActive(true);
                m_missingManaOn = true;
                return;
            }
            else
            {
                m_pointerIndex++;
                return;
            }
        }
    }

    private void ProcessRotation()
    {
        Quaternion playerRot = m_player.transform.rotation;
        Quaternion cameraRot = transform.localRotation;

        Vector3 VecPlayerRot = playerRot.eulerAngles; //Convert the Player Quaternion to Vector3    is needed for Adding the MousMovement
        Vector3 VecCameraRot = cameraRot.eulerAngles; //Convert the Cam Quaternion to Vector3       is needed for Adding the MousMovement

        m_rotX = VecPlayerRot.y + Input.GetAxis("Mouse X") * m_rotationSpeedX * Time.deltaTime; //Get the X-Axis
        VecCameraRot.x = VecCameraRot.x + (-Input.GetAxis("Mouse Y")) * m_rotationSpeedY * Time.deltaTime; //Get the Y-Axis
        VecCameraRot.x = Mathf.Clamp(VecCameraRot.x, 0f, 55f); //Clamp the Y Axis to dont look to high or to low

        VecPlayerRot.y = m_rotX;

        cameraRot = Quaternion.Euler(VecCameraRot); // convert Back to Quaternion
        playerRot = Quaternion.Euler(VecPlayerRot); // convert Back to Quaternion

        transform.localRotation = cameraRot; // rotatet the cam
        m_player.transform.rotation = playerRot; // rotate the player
    }

    private void ChangeView() //set move to the pos where it need to go
    {
        transform.position = Vector3.SmoothDamp(transform.position, m_CamPoses[m_pointerIndex].position, ref Vel, Time.fixedDeltaTime * m_CamMoveSpeed);// moves the cam
        if (m_inMovment && m_pointerIndex == 0)
        {
            Quaternion upPos = Quaternion.Euler(90, 0, 0); //the camera adjust to the correct x-rotation when moving to the UpView
            transform.rotation = Quaternion.Lerp(transform.rotation, upPos, Time.fixedDeltaTime * 3f); // rot the cam while in the UpMove
        }
        if (m_inMovment && m_pointerIndex == 1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, m_CamPoses[m_pointerIndex].rotation, Time.fixedDeltaTime * 3f);
        }
        if (Mathf.RoundToInt(transform.position.y) == Mathf.RoundToInt(m_CamPoses[m_pointerIndex].position.y)) //when the CamMove is fin
        {
            m_inMovment = false;
        }
    }
}
