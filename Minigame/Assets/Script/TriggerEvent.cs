using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public GameObject m_wall;
    private SaveLoadLv1 script_LevelGoals;

    private void Awake()
    {
        script_LevelGoals = FindObjectOfType<SaveLoadLv1>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            WallDestroy();
        }
    }

    // Destroy Wall
    public void WallDestroy()
    {
        Destroy(m_wall);
        script_LevelGoals.lv1_Trigger = 1;
    }
}
