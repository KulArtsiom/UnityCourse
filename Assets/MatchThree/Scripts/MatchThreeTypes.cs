using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchThreeTypes : MonoBehaviour
{
    [SerializeField] private MatchThreeCandyData[] m_CandiesData;
    public MatchThreeCandy GetRandomCandy()
    {
        MatchThreeCandyData candyData = m_CandiesData[Random.Range(0, m_CandiesData.Length)];
        GameObject obj = Instantiate(candyData.Prefab);
        MatchThreeCandy candy = obj.AddComponent<MatchThreeCandy>();
        candy.CandyData = candyData;
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        spriteRenderer.color = candyData.Color;
        
        return candy;
    }
}
