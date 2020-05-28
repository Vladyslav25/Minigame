using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespwanKristal : MonoBehaviour
{
    public GameObject m_Kristal;
    public bool m_isAktiv = true;
    public float m_counter = 4f;
    public GameObject m_parent;
    public Transform m_TransMana;

    private void Update()
    {
        if (!m_isAktiv)
        {
            m_counter -= Time.deltaTime;
            if (Mathf.RoundToInt(m_counter) == 0)
            {
                //Spawn Crystal
                GameObject m_NewKristal = Instantiate(m_Kristal, m_TransMana);
                m_NewKristal.transform.SetParent(m_parent.transform);
                // Reset Spawner
                m_isAktiv = true;
                m_counter = 4f;
            }
        }
    }
}
