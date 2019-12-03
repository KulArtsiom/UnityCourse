using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FingerDriverTrack : MonoBehaviour
{
    public class TrackSegment
    {
        public Vector3[] Points;

        public bool IsPointInSegment(Vector3 point)
        {
            return MathfTriangles.IsPointInTriangleXY(point, Points[0], Points[1], Points[2]);
        }
    }

    [SerializeField] private LineRenderer m_lineRenderer;
    [SerializeField] private bool m_Debug;

    private Vector3[] corners;
    private TrackSegment[] segments;
    private int segmentCounter = 0;
    private int points = 0;
    private int checkPoint = 0;


    // Start is called before the first frame update
    private void Start()
    {
        // Заполняем опорное точки
        corners = new Vector3[this.transform.childCount];
        for (var i = 0; i < corners.Length; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            corners[i] = obj.transform.position;
            //отключение отрисовки объекта/
            obj.GetComponent<MeshRenderer>().enabled = false;
        }

        //настраиваем Line Renderer
        m_lineRenderer.positionCount = corners.Length;
        m_lineRenderer.SetPositions(corners);

        //запекаем меш сетку из Line Renderer
        Mesh mesh = new Mesh();
        m_lineRenderer.BakeMesh(mesh, true);

        //создаем массив сегментов трассы
        //(каждый треугольник описан 3-мя точками из массива вершин)
        segments = new TrackSegment[mesh.triangles.Length / 3];

        //каждую итерацию цикла проходим через каждый треий элемент
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            segments[segmentCounter] = new TrackSegment();

            segments[segmentCounter].Points = new Vector3[3];
            //в первую точку записываем вершину первого индекса из массива вершин 
            segments[segmentCounter].Points[0] = mesh.vertices[mesh.triangles[i]];
            segments[segmentCounter].Points[1] = mesh.vertices[mesh.triangles[i + 1]];
            segments[segmentCounter].Points[2] = mesh.vertices[mesh.triangles[i + 2]];

            segmentCounter++;
        }

        //отдельно можно продебажить сегменты
        if (!m_Debug)
        {
            return;
        }

        foreach (var segment in segments)
        {
            foreach (var point in segment.Points)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = point;
                sphere.transform.localScale = Vector3.one * 0.1f;
            }
            
        }
    }

    private void Update()
    {
        showScores();
    }

    private void showScores()
    {
        print("Scores: " + points);
    }

    /// <summary>
    /// Определяем находится ли точка на трассе
    /// </summary>
    /// <param name="point">точка</param>
    /// <returns></returns>
    public bool IsPointInTrack(Vector3 point)
    {
        for (int i = checkPoint; i <= checkPoint +1; i++)
        {
            if (segments[i].IsPointInSegment(point))
            {
                if (i > checkPoint)
                {
                    checkPoint = i;
                    points = i / 2;
                }
                return true;
            }
        }
        return false;
    }
}