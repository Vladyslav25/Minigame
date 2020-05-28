using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use : MonoBehaviour
{
    public GameObject m_off;
    public GameObject m_on;
    private bool m_pressedE = false;
    private SaveLoadLv2 m_SaveLoad;

    public bool m_isAktiv = false;

    private void Awake()
    {
        if (PlayerPrefs.GetFloat("PositionX") == 0)
        {
            m_on.SetActive(false);
        }
    }
    private void Start()
    {
        m_SaveLoad = FindObjectOfType<SaveLoadLv2>();

    }
    private void Update()
    {
        if (Input.GetButtonDown("E"))
        {
            m_pressedE = true;
        }
        else
        {
            m_pressedE = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && m_pressedE)
        {
            //Open the lever
            if (!m_isAktiv)
            {
                Open();
                if (m_SaveLoad.m_gateAktiv != 2)
                {
                    m_SaveLoad.m_gateAktiv = 2;
                }
                return;
            }
            //Close the lever
            if (m_isAktiv)
            {
                Close();
                if (m_SaveLoad.m_gateAktiv != 1)
                {
                    m_SaveLoad.m_gateAktiv = 1;
                }
                return;
            }
        }
    }

    public void Open()
    {
        m_off.SetActive(false);
        m_on.SetActive(true);
        m_isAktiv = true;
        m_pressedE = false;
    }

    public void Close()
    {
        m_off.SetActive(true);
        m_on.SetActive(false);
        m_isAktiv = false;
        m_pressedE = false;
    }
}
