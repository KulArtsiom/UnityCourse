using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HopPlayer : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_JumpCurve;
    [SerializeField] private float m_JumpHeight = 1;
    [SerializeField] private float m_JumpDistance = 2f;
    [SerializeField] private float m_BallSpeed = 1f;
    [SerializeField] private HopInput m_Input;
    [SerializeField] private HopTrack m_Track;

    private float iteration; //цикл прыжка 
    private float startZ;//точка начала прыжка 
    
    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        
        //смещение
        pos.x = Mathf.Lerp(pos.x, m_Input.Strafe, Time.deltaTime * 5f);
        
        //прыжок 
        pos.y = m_JumpCurve.Evaluate(iteration) * m_JumpHeight;

        //движение вперед
        pos.z = startZ + iteration * m_JumpDistance;
        transform.position = pos;

        iteration += Time.deltaTime * m_BallSpeed;
        if (iteration < 1f)
        {
            return;
        }
        
        iteration = 0;
        startZ += m_JumpDistance;

        if (m_Track.isBallOnPlatform(transform.position))
        {
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
