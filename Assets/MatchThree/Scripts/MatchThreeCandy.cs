using System.Collections;
using System.Collections.Generic;
using MatchThree.Scripts;
using UnityEngine;

public class MatchThreeCandy : MonoBehaviour
{
    public MatchThreeCandyData CandyData;
    private Vector3 baseCandyPosition;
    private bool inDrag;
    private Collider2D currentCollider;

    private void OnMouseDown()
    {
        baseCandyPosition = transform.position;
        inDrag = true;
    }


    private void OnMouseDrag()
    {
        if (!inDrag)
        {
            return;
        }

        var mousePosition = MatchThreeController.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = baseCandyPosition.z;
        //максимальное смещение конфеты
        float maxDistance = MatchThreeField.CurrentCellSize;

        float x = mousePosition.x;
        float baseX = baseCandyPosition.x;
        float y = mousePosition.y;
        float baseY = baseCandyPosition.y;

        //ограничение смещения до одной ячейки
        x = Mathf.Clamp(x, baseX - maxDistance, baseX + maxDistance);
        y = Mathf.Clamp(y, baseY - maxDistance, baseY + maxDistance);
        //ограничение смещения по одной оси
        if (Mathf.Abs(x - baseX) > Mathf.Abs(y - baseY))
        {
            y = baseY;
        }
        else
        {
            x = baseX;
        }


        mousePosition.x = x;
        mousePosition.y = y;

        transform.position = mousePosition;
    }


    private void OnMouseUp()
    {
        inDrag = false;
        if (currentCollider)
        {
            MatchThreeCandy targetCandy = currentCollider.GetComponent<MatchThreeCandy>();
            var targetCell = MatchThreeField.GetCell(targetCandy);
            var currentCell = MatchThreeField.GetCell(this);
            //пытаемся поменять ячейки
            currentCell.Candy = targetCandy;
            targetCell.Candy = this;

            //проверить валидность
            bool isFreePlacement = MatchThreeController.IsFreeCandyPlacement(targetCell, CandyData.Id);
            if (isFreePlacement)
            {
                isFreePlacement = MatchThreeController.IsFreeCandyPlacement(currentCell, targetCandy.CandyData.Id);
            }

            //если смена НЕ приведет к совпадению 3-х
            if (isFreePlacement)
            {
                targetCell.Candy = targetCandy;
                currentCell.Candy = this;
                transform.position = baseCandyPosition;
                return;
            }

            //Если приведет к совпадению 3-х
            transform.position = targetCell.transform.position;
            targetCandy.transform.position = currentCell.transform.position;
            return;
        }
        
        transform.position = baseCandyPosition;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        currentCollider = other;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentCollider == other)
        {
            currentCollider = null;
        }
    }
}