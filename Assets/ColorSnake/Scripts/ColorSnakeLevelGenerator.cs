using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ColorSnake.Scripts;
using UnityEngine;

public class ColorSnakeLevelGenerator : MonoBehaviour
{
    [SerializeField] private ColorSnakeTypes m_Types;
    [SerializeField] private ColorSnakeGameController m_Controller;

    private int line = 1;//номер генерируей линии препятствий
    private List<GameObject> obstacles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        var upBorder = m_Controller.Bounds.Up;
        while (line * 2f < upBorder + 2f)
        {
            GenerateObstacles();
        }
    }

    void Update()
    {
        //верхняя позиция 
        var upBorder = m_Controller.Bounds.Up + m_Controller.MainCamera.transform.position.y;
        if (line * 2f > upBorder + 2f)
            return;

        GenerateObstacles();
        //
        var downBorder = m_Controller.MainCamera.transform.position.y + m_Controller.Bounds.Down;
        
        if(obstacles[0].transform.position.y + 4f < downBorder)
        {
            //удаляем преграду
            Destroy(obstacles[0]);
            //удаляем из массива
            obstacles.RemoveAt(0);
        }
        
    }
    private void GenerateObstacles()
    {
        TemplateType template;
        ObjectType objType;
        ColorType colorType;

        if (line == 20)
        {
            template = m_Types.GetTemplateByName("SuperLine");
        }
        else
        {
            template = m_Types.GetRandomTemplateType();
        }
        
        objType = m_Types.GetRandomObjectType();
        colorType = m_Types.GetRandomColorType();
        var obstacle = new GameObject("Obstacle_" + line);
        
        
        foreach (var point in template.points)
        {
            var obj = Instantiate(objType.Object, point.transform.position, point.transform.rotation);
            obj.transform.parent = obstacle.transform;
            obj.GetComponent<SpriteRenderer>().color = colorType.Color;
            var obstacleController = obj.AddComponent<ColorSnakeObstacle>();
            
            obstacleController.ColorId = colorType.Id;
            obstacleController.Name = template.Name;
        }

        Vector3 pos = obstacle.transform.position;
        pos.y = line * 3f;//2 - расстояние между препятсвиями
        obstacle.transform.position = pos;

        line++;

        obstacles.Add(obstacle);
    }
}

