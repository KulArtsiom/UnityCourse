using System;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Scripts
{
    public class MatchThreeField : MonoBehaviour
    {
        [SerializeField] private Camera m_MainCamera;
        [SerializeField] private GameObject m_Cell;
        [SerializeField] private float m_CellSize = 0.6f;
        [SerializeField] private int m_FieldWidth = 6;
        [SerializeField] private int m_Fieldheight = 8;

        private static readonly List<List<MatchThreeCell>> GameField = new List<List<MatchThreeCell>>();
        public static float CurrentCellSize;

        public void Init()
        {
            GenerateFiled(m_FieldWidth, m_Fieldheight);
            CurrentCellSize = m_CellSize;
        }

        private void GenerateFiled(int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                GameField.Add(new List<MatchThreeCell>());
                for (int y = 0; y < height; y++)
                {
                    Vector3 pos = new Vector3(x * m_CellSize, y * m_CellSize, 0f);
                    var obj = Instantiate(m_Cell, pos, Quaternion.identity);
                    obj.name = $"Cell{x}{y}";

                    var cell = obj.AddComponent<MatchThreeCell>();

                    GameField[x].Add(cell);

                    //натсройка соседей по горизонтали

                    //если не крайняя левая клетка 
                    if (x > 0)
                    {
                        cell.SetNeighbour(Direction.Left, GameField[x - 1][y]);
                        GameField[x - 1][y].SetNeighbour(Direction.Right, cell);
                    }

                    //если не самая нижняя
                    if (y > 0)
                    {
                        cell.SetNeighbour(Direction.Down, GameField[x][y - 1]);
                        GameField[x][y - 1].SetNeighbour(Direction.Up, cell);
                    }
                }
            }

            m_MainCamera.transform.position = new Vector3(width * m_CellSize * 0.5f, height * m_CellSize * 0.5f, -1);
        }

        public static MatchThreeCell GetCell(MatchThreeCandy candy)
        {
            foreach (var row in GameField)
            {
                foreach (var cell in row)
                {
                    if (cell.Candy == candy)
                    {
                        return cell;
                    }
                }
            }
            return null;
        }
        
        public static MatchThreeCell GetCell(int x, int y)
        {
            return GameField[x][y];
        }
        
        
    }
}