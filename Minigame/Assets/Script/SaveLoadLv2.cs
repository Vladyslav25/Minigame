using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadLv2 : MonoBehaviour
{
    private TriggerEvent script_lvTrigger;
    private Movement script_Movemnt;
    private LevelChanger script_LevelChanger;
    private Use script_Gate;

    public GameObject m_player;
    public int m_gateAktiv = 0;

    private void Awake()
    {
        script_lvTrigger = FindObjectOfType<TriggerEvent>();
        script_Movemnt = FindObjectOfType<Movement>();
        script_LevelChanger = FindObjectOfType<LevelChanger>();
        script_Gate = FindObjectOfType<Use>();

        if (PlayerPrefs.HasKey("PositionX"))
        {
            Load();
        }
    }

    public void Load()
    {
        Vector3 Pos = new Vector3(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"), PlayerPrefs.GetFloat("PositionZ"));
        Quaternion Rotation = new Quaternion(PlayerPrefs.GetFloat("RotationX"), PlayerPrefs.GetFloat("RotationY"),
            PlayerPrefs.GetFloat("RotationZ"), PlayerPrefs.GetFloat("RotationW"));
        //Player Pos and Rot
        m_player.transform.position = Pos;
        m_player.transform.rotation = Rotation;
        //Player Stats
        script_Movemnt.ManaKristal = PlayerPrefs.GetInt("Mana");
        script_Movemnt.Stamina = PlayerPrefs.GetFloat("Stamina");
        //Gate
        if (PlayerPrefs.GetInt("Gate") == 2)
        {
            script_Gate.Open();
            m_gateAktiv = 2;
        }
        else
        {
            script_Gate.Close();
            m_gateAktiv = 1;
        }

    }

    public void Save()
    {
        //Player Pos
        PlayerPrefs.SetFloat("PositionX", m_player.transform.position.x);
        PlayerPrefs.SetFloat("PositionY", m_player.transform.position.y);
        PlayerPrefs.SetFloat("PositionZ", m_player.transform.position.z);
        //Player Rot
        PlayerPrefs.SetFloat("RotationX", m_player.transform.rotation.x);
        PlayerPrefs.SetFloat("RotationY", m_player.transform.rotation.y);
        PlayerPrefs.SetFloat("RotationZ", m_player.transform.rotation.z);
        PlayerPrefs.SetFloat("RotationW", m_player.transform.rotation.w);
        //Player Stats
        PlayerPrefs.SetInt("Mana", script_Movemnt.ManaKristal);
        PlayerPrefs.SetFloat("Stamina", script_Movemnt.Stamina);
        PlayerPrefs.SetInt("Level", script_LevelChanger.m_currentLevel);
        //Gate
        PlayerPrefs.SetInt("Gate", m_gateAktiv);

        PlayerPrefs.Save();
    }
}
