using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKristal : MonoBehaviour
{
    public GameObject m_toDestroy;
    private Movement script_Movment;
    private AudioSource m_coinSound;
    private RespwanKristal script_Respawn;

    private void Awake()
    {
        script_Respawn = FindObjectOfType<RespwanKristal>();
        script_Movment = FindObjectOfType<Movement>();
        m_coinSound = GameObject.FindGameObjectWithTag("AudioMana").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            //Add mana
            script_Movment.ManaKristal += 1;
            //Play sound
            m_coinSound.Play();
            //Reset Spawner
            script_Respawn.m_isAktiv = false;
            //Destroy
            Destroy(m_toDestroy);
        }
    }
}
