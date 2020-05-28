using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esc : MonoBehaviour
{
    private LevelChanger script_LevelChanger;

    private void Awake()
    {
        script_LevelChanger = FindObjectOfType<LevelChanger>();
    }

    void Update()
    {
        //Open MainMenu
        if (Input.GetButtonDown("HM"))
        {
            script_LevelChanger.HM();
        }
    }
}
