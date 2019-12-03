using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopPlatform : MonoBehaviour
{
    [SerializeField] private GameObject m_DefaultPlatform;
    [SerializeField] private GameObject m_GreenPlatform;


    private void Start()
    {
        SetBase();
    }

    public void SetGreen()
    {
        m_DefaultPlatform.SetActive(false);
        m_GreenPlatform.SetActive(true);

        Invoke(nameof(SetBase), 0.5f);
    }

    private void SetBase()
    {
        m_DefaultPlatform.SetActive(true);
        m_GreenPlatform.SetActive(false);
    }
}