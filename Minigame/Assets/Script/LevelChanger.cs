using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private string[] m_levelName = new string[4] { "MainM", "Level_1", "Level_2", "Level_3" };
    public int m_currentLevel;

    private Next_Level script_nextLevel;

    private void Awake()
    {
        script_nextLevel = FindObjectOfType<Next_Level>();
        if (script_nextLevel != null)
        {
            m_currentLevel = script_nextLevel.m_currLvNumm;
        }
    }

    //Change Level
    public void NextLevel(int _currLvNumm)
    {
        m_currentLevel = _currLvNumm;
        _currLvNumm++;
        SceneManager.LoadScene(m_levelName[_currLvNumm]);
    }

    //Open MainMenu
    public void HM()
    {
        SceneManager.LoadScene(m_levelName[0]);
    }
}
