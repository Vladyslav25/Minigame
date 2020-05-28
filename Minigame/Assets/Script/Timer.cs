using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text m_manaText;
    public RectTransform m_manaBar;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    //Hide Time UI
    public void Hide()
    {
        gameObject.SetActive(false);
        m_manaText.gameObject.SetActive(false);
    }

    //Show Timer UI
    public void Show()
    {
        gameObject.SetActive(true);
        m_manaBar.gameObject.SetActive(true);
        m_manaText.gameObject.SetActive(true);
    }
}
