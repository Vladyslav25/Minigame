using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaKristalOnCollision : MonoBehaviour
{
    private Movement m_playerMovment;
    public GameObject m_Kristal;
    public AudioSource m_coinSound;

    private void Awake()
    {
        m_playerMovment = FindObjectOfType<Movement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Add Crystal to Player
        m_playerMovment.ManaKristal += 1;
        //Player Sound
        m_coinSound.Play();
        //Destroy
        Destroy(m_Kristal);
    }
}
