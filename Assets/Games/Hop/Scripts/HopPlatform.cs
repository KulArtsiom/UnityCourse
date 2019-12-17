using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HopPlatform : MonoBehaviour
{
    [SerializeField] private GameObject m_DefaultPlatform;
    [SerializeField] private GameObject m_GreenPlatform;
    [SerializeField] private GameObject m_RedPlatform;
    [SerializeField] private GameObject m_PurplePlatform;
    [SerializeField] private GameObject m_Turquoiselatform;

    
    public string platformName = "def";
    
    public void SetGreen()
    {
        m_DefaultPlatform.SetActive(false);
        m_GreenPlatform.SetActive(true);
        m_RedPlatform.SetActive(false);
        m_Turquoiselatform.SetActive(false);
        m_PurplePlatform.SetActive(false);
        

        Invoke(nameof(SetBase), 0.5f);
    }

    public void SetRed()
    {
        m_DefaultPlatform.SetActive(false);
        m_RedPlatform.SetActive(true);
        m_GreenPlatform.SetActive(false);
        m_Turquoiselatform.SetActive(false);
        m_PurplePlatform.SetActive(false);
  
        Invoke(nameof(SetBase), 0.5f);
    }

    public void setPurple()
    {
        platformName = "Purple";
        m_DefaultPlatform.SetActive(false);
        m_GreenPlatform.SetActive(false);
        m_RedPlatform.SetActive(false);
        m_Turquoiselatform.SetActive(false);
        m_PurplePlatform.SetActive(true);

        Invoke(nameof(SetBase), 0.5f);
    }

    public void SetTurquoise()
    {
        platformName = "Turquoise";
        m_DefaultPlatform.SetActive(false);
        m_GreenPlatform.SetActive(false);
        m_RedPlatform.SetActive(false);
        m_Turquoiselatform.SetActive(true);
        m_PurplePlatform.SetActive(false);

        Invoke(nameof(SetBase), 0.5f);
    }

    private void SetBase()
    {
        m_DefaultPlatform.SetActive(true);
        m_GreenPlatform.SetActive(false);
        m_RedPlatform.SetActive(false);
    }
}