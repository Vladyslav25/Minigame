using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadLv1 : MonoBehaviour
{
    private TriggerEvent script_lvTrigger;
    private Movement script_Movemnt;
    private LevelChanger script_LevelChanger;

    public int lv1_Trigger = 0;
    public int lv1_CollectMana = 0;

    public GameObject m_player;
    public GameObject ManaKristal;

    private void Awake()
    {
        script_lvTrigger = FindObjectOfType<TriggerEvent>();
        script_Movemnt = FindObjectOfType<Movement>();
        script_LevelChanger = FindObjectOfType<LevelChanger>();

        if (PlayerPrefs.HasKey("PositionX"))
        {
            Load();
        }
    }

    private void Update()
    {
        if (script_Movemnt.ManaKristal == 1)
        {
            //Level
            PlayerPrefs.SetInt("CollectMana", 1);
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
        //Level
        if (PlayerPrefs.GetInt("Level1_Trigger") == 1)
        {
            script_lvTrigger.WallDestroy();
        }
        if (PlayerPrefs.GetInt("CollectMana") == 1)
        {
            Destroy(ManaKristal);
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
        //Level
        PlayerPrefs.SetInt("Level1_Trigger", lv1_Trigger);

        PlayerPrefs.Save();
    }
}
