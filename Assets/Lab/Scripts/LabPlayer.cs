using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LabPlayer : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_JumpCurve;
    [SerializeField] private float m_JumpDistance = 2f;
    [SerializeField] private LabInput m_Input;
    [SerializeField] private LabTrack m_Track;

    [SerializeField] private Button btnRestart;
    [SerializeField] private Text textGameOver;
    [SerializeField] private Text textWin;

    private float iteration; //цикл прыжка 
    private float startZ; //точка начала прыжка 
    private float speed = 1f; //дефолтная скорость
    private float height = 1f;

    private int lifes = 3;
    private int gates = 0;

    private bool isGame;


    private void Start()
    {
        textGameOver.gameObject.SetActive(false);
        btnRestart.gameObject.SetActive(false);
        textWin.gameObject.SetActive(false);

        isGame = true;

        LabScores.scoreValue = lifes;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGame)
        {
            LabScores.scoreValue = lifes;
            var pos = transform.position;

            //смещение
            pos.x = Mathf.Lerp(pos.x, m_Input.Strafe, Time.deltaTime * 5f);

            //прыжок 
            pos.y = m_JumpCurve.Evaluate(iteration) * height;

            //движение вперед
            pos.z = startZ + iteration * m_JumpDistance;
            transform.position = pos;


            iteration += Time.deltaTime * speed;

            if (iteration < 1f)
            {
                return;
            }

            iteration = 0;
            startZ += m_JumpDistance;

            if (m_Track.isBallOnPlatform(transform.position))
            {
                if (gates >= 10)
                {
                    speed = .0f;
                    height = .0f;
                    lifes = 0;
                    textWin.gameObject.SetActive(true);
                    isGame = false;
                }
                
                speed = m_Track.speed;
                height = m_Track.height;
                gates += 1;
                LabScoreNew.scoreValueGates = gates;
            }
            else
            {
                lifes -= 1;
                if (lifes <= 0)
                {
                    LabScores.scoreValue = lifes;
                    pos = transform.position;
                    speed = .0f;
                    height = .0f;
                    lifes = 0;
                    btnRestart.gameObject.SetActive(true);
                    isGame = false;
                }
            }
        }
    }
}