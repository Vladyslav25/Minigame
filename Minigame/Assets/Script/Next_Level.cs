using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_Level : MonoBehaviour
{
    LevelChanger script_levelChanger;
    public int m_currLvNumm;

    private void Awake()
    {
        script_levelChanger = FindObjectOfType<LevelChanger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            PlayerPrefs.DeleteAll();
            //Change Level
            script_levelChanger.NextLevel(m_currLvNumm);
        }
    }
}
