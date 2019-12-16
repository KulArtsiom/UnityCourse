using System;
using System.Collections;
using System.Collections.Generic;
using ColorSnake.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorSnakeSnake : MonoBehaviour
{
    [SerializeField] private ColorSnakeGameController m_GameController;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private Button btnRestart;
    [SerializeField] private Text textGameOver;
    


    private Vector3 position;
    private int currentType;
    private bool isGameOver;
    

    private void Start()
    {
        ColorSnakeScore.scoreValue = 0;
        position = transform.position;
        isGameOver = false;
        btnRestart.gameObject.SetActive(false);
        textGameOver.gameObject.SetActive(false);
        //получаем рандомный цвет на старте игры
        var colorType = m_GameController.Types.GetRandomColorType();
        //запоминаем id йцвета
        currentType = colorType.Id;
        m_SpriteRenderer.color = colorType.Color;
    }

    private void setColor(int id)
    {
        var colorType = m_GameController.Types.GetColorType(id);
        //запоминаем id йцвета
        currentType = colorType.Id;
        m_SpriteRenderer.color = colorType.Color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var obstacle = other.gameObject.GetComponent<ColorSnakeObstacle>();
        if (!obstacle)
        {
            return;
        }

        if (!obstacle.Name.Equals("SuperLine"))
        {
            if (currentType != obstacle.ColorId)
            {
                isGameOver = true;
                btnRestart.gameObject.SetActive(true);
                textGameOver.gameObject.SetActive(true);
            }
            else
            {
                ColorSnakeScore.scoreValue += 1;
                isGameOver = false;
            }
        }
       
        
        setColor(obstacle.ColorId);
        Destroy(obstacle.gameObject);
    }

    void Update()
    {
        if (!isGameOver)
        {
            position = transform.position;
            if (!Input.GetMouseButton(0))
            {
                return;
            }

            position.x = m_GameController.MainCamera.ScreenToWorldPoint(Input.mousePosition).x;

            //отнять размеры змеи
            float min = m_GameController.Bounds.Left;
            float max = m_GameController.Bounds.Right;

            position.x = Mathf.Clamp(position.x, min, max);
            transform.position = position;
        }
        else
        {
            transform.position = position;
        }
    }
}