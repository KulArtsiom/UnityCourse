using System;
using UnityEngine;


namespace FinInTheHole.Scripts
{
    public class FitInTheHole_LevelGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject m_CubePrefab;
        [SerializeField] private float m_Speed = 20f;
        [SerializeField] private float m_WallDistance = 35f;
        [SerializeField] private FitInTheHole_Template[] m_TemplatePrefabs;
        [SerializeField] private Transform m_FigurePoint;

        private FitInTheHole_Template[] templates;// храним экземпляры чтобы не инстансировать кажлый раз
        private FitInTheHole_Template figure; // текущая фигура
        


        private float speed;
        private FitInTheHole_Wall wall;

        private void Start()
        {
            templates = new FitInTheHole_Template[m_TemplatePrefabs.Length];
            for (int i = 0; i < templates.Length; i++)
            {
                templates[i] = Instantiate(m_TemplatePrefabs[i]);
                templates[i].gameObject.SetActive(false);
                templates[i].transform.position = m_FigurePoint.position;
            }
            
            wall = new FitInTheHole_Wall(5,5, m_CubePrefab);
            SetupTemplate();
            wall.SetUpWall(figure, m_WallDistance);
            speed = m_Speed;
        }

        private void Update()
        {
            wall.Parent.transform.Translate(speed * Time.deltaTime * Vector3.back);

            if (wall.Parent.transform.position.z > m_WallDistance *-1f)
            {
                return;
            }
            
            SetupTemplate();
            wall.SetUpWall(figure, m_WallDistance);
        }

        private void SetupTemplate()
        {
            if (figure)
            {
                figure.gameObject.SetActive(false);
            }

            var rand = UnityEngine.Random.Range(0, templates.Length);
            figure = templates[rand];
            figure.gameObject.SetActive(true);
            figure.SetupRandomFigure();
        }
    }
}