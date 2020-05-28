using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainM_Buttons : MonoBehaviour
{
    LevelChanger m_nextLevel;
    public GameObject m_LoadButton;
    public GameObject m_DelButton;

    private void Awake()
    {
        m_nextLevel = FindObjectOfType<LevelChanger>();
    }
    private void Update()
    {
        //if PlayerPrefabs have something saved
        if (!PlayerPrefs.HasKey("PositionX"))
        {
            m_LoadButton.SetActive(false);
            m_DelButton.SetActive(false);
        }
        else
        {
            m_LoadButton.SetActive(true);
            m_DelButton.SetActive(true);
        }
    }

    public void StartClick()
    {
        PlayerPrefs.DeleteAll();
        m_nextLevel.NextLevel(0);
    }
    public void EndGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }
    public void DelGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
