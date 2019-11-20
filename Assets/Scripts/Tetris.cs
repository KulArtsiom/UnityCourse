using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tetris : MonoBehaviour
{
    [SerializeField] private GameObject m_RedCube;
    [SerializeField] private GameObject m_PurpleBeam;
    [SerializeField] private GameObject m_Enemy;
    [SerializeField] private GameObject m_Enemy_Steal;
    [SerializeField] private GameObject m_BlueCube;
    [SerializeField] private GameObject m_LemonBeam;
    [SerializeField] private GameObject m_Enemy_Black;
    [SerializeField] private GameObject m_Game_Over;


    private GameObject redCube;
    private GameObject purpleBeam;
    private GameObject enemy;
    private GameObject enemySteal;
    private GameObject blueCube;
    private GameObject lemonBeam;
    private GameObject enemyBlack;
    private GameObject tGameOver;

    private bool isfirstObjectFinished;
    private bool isSecondObjectFinished;
    private bool isThirdObjectFinished;
    private bool isFourthObjectFinished;
    private bool isFifthObjectFinished;
    private bool isSixObjectFinished;
    private bool isSeventhObjectFinished;

    private Vector3 startPositionCube = new Vector3(-0.5f, 10.0f, 1.0f);
    private Vector3 endPositionCube = new Vector3(0.5f, -2.5f, 1.0f);
    
    private Vector3 endPositionBeam = new Vector3(0.0f, -1.0f, 1.0f);

    private Vector3 startPositionEnemy = new Vector3(-1.5f, 10.0f, 1.0f);
    private Vector3 endPositionEnemy = new Vector3(0.0f, -0.5f, 1.0f);

    private Vector3 startPositionEnemySteal = new Vector3(1.5f, 10.0f, 1.0f);
    private Vector3 endPositionEnemySteal = new Vector3(0.0f, 1.5f, 1.0f);

    private Vector3 startPositionBlueCube = new Vector3(1.5f, 10.0f, 1.0f);
    private Vector3 endPositionBlueCube = new Vector3(0.0f, 3.5f, 1.0f);

    private Vector3 startPositionPurpleLemonBeam = new Vector3(0.5f, 10.0f, 1.0f);
    private Vector3 endPositionPurpleLemonBeam = new Vector3(0.0f, 4.5f, 1.0f);

    private Vector3 startPositionPurpleEnemyBlack = new Vector3(1.5f, 10.0f, 1.0f);
    private Vector3 endPositionPurpleEnemyBlack = new Vector3(0.0f, 6.5f, 1.0f);
    
    private Vector3 textPosition = new Vector3(-3.3f, 3f, 1.0f);

    private float speed = 0.1f;

    void Start()
    {
    
        purpleBeam = Instantiate(m_PurpleBeam);
        redCube = Instantiate(m_RedCube);
        enemy = Instantiate(m_Enemy);
        enemySteal = Instantiate(m_Enemy_Steal);
        blueCube = Instantiate(m_BlueCube);
        lemonBeam = Instantiate(m_LemonBeam);
        enemyBlack = Instantiate(m_Enemy_Black);
        tGameOver = Instantiate(m_Game_Over);
        
        purpleBeam.transform.position = startPositionCube;
        redCube.transform.position = startPositionCube;
        enemy.transform.position = startPositionEnemy;
        enemySteal.transform.position = startPositionEnemySteal;
        blueCube.transform.position = startPositionBlueCube;
        lemonBeam.transform.position = startPositionPurpleLemonBeam;
        enemyBlack.transform.position = startPositionPurpleEnemyBlack;
        tGameOver.transform.position = textPosition;

        redCube.SetActive(false);
        enemy.SetActive(false);
        enemySteal.SetActive(false);
        blueCube.SetActive(false);
        lemonBeam.SetActive(false);
        enemyBlack.SetActive(false);
        tGameOver.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPositionPurpleBeam = purpleBeam.transform.position;
        if (currentPositionPurpleBeam.y >= endPositionCube.y)
        {
            currentPositionPurpleBeam.y -= speed;
            purpleBeam.transform.position = currentPositionPurpleBeam;
        }
        else
        {
            redCube.SetActive(true);
            isfirstObjectFinished = true;
        }

        if (isfirstObjectFinished)
        {
            Vector3 currentPositionRedCube = redCube.transform.position;
            if (currentPositionRedCube.y >= endPositionBeam.y)
            {
                currentPositionRedCube.y -= speed;
                redCube.transform.position = currentPositionRedCube;
            }
            else
            {
                enemy.SetActive(true);
                isSecondObjectFinished = true;
            }
        }

        if (isSecondObjectFinished)
        {
            Vector3 currentPositionEnemy = enemy.transform.position;
            if (currentPositionEnemy.y >= endPositionEnemy.y)
            {
                currentPositionEnemy.y -= speed;
                enemy.transform.position = currentPositionEnemy;
            }
            else
            {
                enemySteal.SetActive(true);
                isFourthObjectFinished = true;
            }
        }

        if (isFourthObjectFinished)
        {
            Vector3 currentPositionEnemySteal = enemySteal.transform.position;
            if (currentPositionEnemySteal.y >= endPositionEnemySteal.y)
            {
                currentPositionEnemySteal.y -= speed;
                enemySteal.transform.position = currentPositionEnemySteal;
            }
            else
            {
                blueCube.SetActive(true);
                isFifthObjectFinished = true;
            }
        }

        if (isFifthObjectFinished)
        {
            Vector3 currentPositionBlueCube = blueCube.transform.position;
            if (currentPositionBlueCube.y >= endPositionBlueCube.y)
            {
                currentPositionBlueCube.y -= speed;
                blueCube.transform.position = currentPositionBlueCube;
            }
            else
            {
                lemonBeam.SetActive(true);
                isSixObjectFinished = true;
            }
        }

        if (isSixObjectFinished)
        {
            Vector3 currentPositionDoubleBeam = lemonBeam.transform.position;
            if (currentPositionDoubleBeam.y >= endPositionPurpleLemonBeam.y)
            {
                currentPositionDoubleBeam.y -= speed;
                lemonBeam.transform.position = currentPositionDoubleBeam;
            }
            else
            {
                enemyBlack.SetActive(true);
                isSeventhObjectFinished = true;
            }
        }

        if (isSeventhObjectFinished)
        {
            Vector3 currentPositionEnemyBlack = enemyBlack.transform.position;
            if (currentPositionEnemyBlack.y >= endPositionPurpleEnemyBlack.y)
            {
                currentPositionEnemyBlack.y -= speed;
                enemyBlack.transform.position = currentPositionEnemyBlack;
            }
            else
            {
                redCube.SetActive(false);
                purpleBeam.SetActive(false);
                enemy.SetActive(false);
                enemySteal.SetActive(false);
                blueCube.SetActive(false);
                lemonBeam.SetActive(false);
                enemyBlack.SetActive(false);
                tGameOver.SetActive(true);
            }
        }
    }
}