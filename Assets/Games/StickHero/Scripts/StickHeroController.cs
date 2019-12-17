using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class StickHeroController : MonoBehaviour
{
    [SerializeField] private StickHeroStick m_Stick;
    [SerializeField] private StickHeroPlayer m_Player;
    [SerializeField] private List<StickHeroPlatform> m_Platforms;
    [SerializeField] private StickHeroPlatform m_Platform;

    private int counter; //счетчик платформ
    private StickHeroPlatform stickHeroPlatform;
    
    private enum EGameState
    {
        Wait,
        Scaling,
        Rotate,
        Movement,
        Defeat,
    }

    private EGameState currentGameState;
    
    // Start is called before the first frame update
    private void Start()
    {
        currentGameState = EGameState.Wait;
        counter = 0;
        
        m_Stick.ResetStick(m_Platforms[0].GetStickPosition());
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        switch (currentGameState)
        {
            case EGameState.Wait:
                currentGameState = EGameState.Scaling;
                m_Stick.StartScaling();
                break;
            
            case EGameState.Scaling:
                currentGameState = EGameState.Rotate;
                m_Stick.StopScaling();
                break;
            
            case EGameState.Rotate:
            case EGameState.Movement:
                break;
            
            case EGameState.Defeat:
               
                print("Game restarted");
                int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(activeSceneIndex);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void StopStickScale()
    {
        currentGameState = EGameState.Rotate;
        m_Stick.StartRotate();
    }

    //TODO выкинуть?
    public void StopStickRotate()
    {
        currentGameState = EGameState.Movement;
    }

    public void StartPlayerMovement(float length)
    {
        currentGameState = EGameState.Movement;
        StickHeroPlatform nextPlatform = m_Platforms[counter + 1];
        
        //находим минимальную длину стика для успешного перехода
        float targetLength = nextPlatform.transform.position.x -
                             m_Stick.transform.position.x;
        float platformSize = nextPlatform.GetPlatformSize();
        
        float min = targetLength - platformSize * 0.5f;
        min -= m_Player.transform.localScale.x;
        
        //находим максимальную длину стика
        float max = targetLength + platformSize * 0.5f;
        
        //при успехе игрок переходит в центр платформы, иначе падаем
        if (length > min && length < max)
        {
            m_Player.StartMovement(
                nextPlatform.transform.position.x, false);
        }
        else
        {
            float targetPos = m_Stick.transform.position.x +
                              length + m_Player.transform.localScale.x;
            
            m_Player.StartMovement(targetPos, true);
        }
    }

    public void StopPlayerMovement()
    {
        currentGameState = EGameState.Wait;
        counter++;
        GeneratePlatform();
        m_Stick.ResetStick(m_Platforms[counter].GetStickPosition());
    }
    
    public void GeneratePlatform()
    {
        stickHeroPlatform = Instantiate(m_Platform);
        float value = Random.Range(0.5f, 1.5f);
        stickHeroPlatform.transform.position = new Vector3(
            m_Platforms[counter].transform.position.x 
            + value, m_Platforms[counter].transform.position.y);
        float valueX = Random.Range(0.2f, 0.7f);
        stickHeroPlatform.transform.localScale = new Vector3(x: valueX, y: 1.0f, z: 1.0f);
        m_Platforms.Add(stickHeroPlatform);
    }

    public void ShowScores()
    {
        currentGameState = EGameState.Defeat;
        print($"Game over {counter}");
    }
}
