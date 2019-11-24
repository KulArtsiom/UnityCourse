using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StickHeroController : MonoBehaviour
{
    [SerializeField] private StickHeroStick m_Stick;
    [SerializeField] private StickHeroPlayer m_Player;
    [SerializeField] private StickHeroPlatform[] m_Platforms;

    /*counter platfrom*/
    private int counter;

    private enum EGameState
    {
        Wait,
        Scaling,
        Rotate,
        Movement,
        Defeat
    }

    private EGameState currentgameState;


    // Start is called before the first frame update
    private void Start()
    {
        currentgameState = EGameState.Wait;
        counter = 0;
        m_Stick.ResetStick(m_Platforms[0].GetStickPosition());
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        switch (currentgameState)
        {
            case EGameState.Wait:
                currentgameState = EGameState.Scaling;
                m_Stick.StartScaling();
                break;
            case EGameState.Scaling:
                currentgameState = EGameState.Rotate;
                m_Stick.StopScaling();
                break;
            case EGameState.Rotate:
            case EGameState.Movement:
                break;

            case EGameState.Defeat:
                print("GAME RESTARTED");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }

    public void StopStickScale()
    {
        currentgameState = EGameState.Rotate;
        m_Stick.StartRotate();
    }

    //TODO remove?
    public void StopStickRotate()
    {
        currentgameState = EGameState.Movement;
    }

    public void StartPlayerMovement(float length)
    {
        currentgameState = EGameState.Movement;
        StickHeroPlatform nextPlatform = m_Platforms[counter + 1];

        //find min length for successful move
        float targetLength = nextPlatform.transform.position.x - m_Stick.transform.position.x;
        float platformSize = nextPlatform.GetPlatformSize();

        float min = targetLength - platformSize * 0.5f;
        min -= m_Player.transform.localScale.x;

        //find max length for successful
        float max = targetLength + platformSize * 0.5f;

        //if successful moving to center next platform else fall 
        if (length > min && length < max)
        {
            m_Player.StartMovement(nextPlatform.transform.position.x, false);
        }
        else
        {
            float targetPosition = m_Stick.transform.position.x + length + m_Player.transform.localScale.x;
            m_Player.StartMovement(targetPosition, true);
        }
    }

    public void StopPlayerMovement()
    {
        currentgameState = EGameState.Wait;
        counter++;
        m_Stick.ResetStick(m_Platforms[counter].GetStickPosition());
    }

    public void ShowScores()
    {
        currentgameState = EGameState.Defeat;
        print($"Game over{counter}");
    }
}