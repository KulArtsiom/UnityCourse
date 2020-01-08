using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public static float HorizontalAxis;
    public static event Action<float> JumpAction;
    public static event Action<string> FireAction;

    private float jumpTimer;
    private Coroutine waitForJumpCoroutine;
    
    private void Start()
    {
        HorizontalAxis = 0f;
    }

    private void OnDestroy()
    {
        HorizontalAxis = 0f;
    }

    private void Update()
    {
        HorizontalAxis = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (waitForJumpCoroutine == null)
            {
                waitForJumpCoroutine = StartCoroutine(WaitJump());
                return;
            }

            jumpTimer = Time.time;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            FireAction?.Invoke("Fire1");
        }
        
        if (Input.GetButtonDown("Fire2"))
        {
            FireAction?.Invoke("Fire2");
        }
        
    }

    public IEnumerator WaitJump()
    {
        //ожидаем второго нажатия на прыжок
        yield return new WaitForSeconds(0.2f);
        if (JumpAction !=null)
        {
            //должно быть в константе
            var force = Time.time - jumpTimer < 0.2f ? 1.25f : 1f; 
            JumpAction.Invoke(force);
        }

        waitForJumpCoroutine = null;
    }
}
