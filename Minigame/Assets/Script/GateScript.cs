using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public GameObject[] m_gates;
    private Use script_lever;

    private void Awake()
    {
        script_lever = FindObjectOfType<Use>();
    }

    private void Update()
    {
        //Open Gate
        if (script_lever.m_isAktiv)
        {
            foreach (GameObject g in m_gates)
            {
                g.gameObject.SetActive(false);
            }
            return;
        }
        //Close Gate
        if (!script_lever.m_isAktiv && !m_gates[1].gameObject.activeSelf)
        {
            foreach(GameObject g in m_gates)
            {
                g.gameObject.SetActive(true);
            }
        }
    }

}
