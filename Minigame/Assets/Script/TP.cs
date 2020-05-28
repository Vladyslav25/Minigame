using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TP : MonoBehaviour
{
    public GameObject m_player;
    public Transform m_TpPartener;
    public AudioSource m_clipTP;

    private void Awake()
    {
        m_player = GameObject.Find("Player");
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //Teleport to Partner Teleporter
            m_player.transform.position = m_TpPartener.position;
            m_player.transform.rotation = m_TpPartener.rotation;
            //Play Sound
            m_clipTP.time = 0.3f;
            m_clipTP.Play();
        }
    }

}