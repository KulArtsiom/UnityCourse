using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : CharacterMovement
{
    [SerializeField] private Transform graphics;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Animator animator;

    private Rigidbody2D rig;
    private bool isRun;
    private float maxSpeed;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int JumpInt = Animator.StringToHash("Jump");
    private static readonly int JumpPlayer = Animator.StringToHash("JumpPlayer");
    private static readonly int Run = Animator.StringToHash("Run");

    private void Start()
    {
        maxSpeed = 2f;
        rig = GetComponent<Rigidbody2D>();
        InputController.JumpAction += OnJumpAction;
    }

    private void OnDestroy()
    {
        InputController.JumpAction -= OnJumpAction;
    }

    private void FixedUpdate()
    {
        if (IsFreezing)
        {
            Vector2 velocity = rig.velocity;
            velocity.x = 0f;
            rig.velocity = velocity;
            return;
        }

        var direction = new Vector2(InputController.HorizontalAxis, 0f);

        if (!IsGrounded())
        {
            direction *= 0.5f;
        }

        Move(direction);
    }

    private void Update()
    {
        if (IsGrounded())
        {
            animator.SetFloat(Speed, Mathf.Abs(rig.velocity.x));
            if (isRun)
            {
                animator.SetFloat(Speed, Mathf.Abs(rig.velocity.x));
                maxSpeed = 5f;
            }
            else
            {
                maxSpeed = 2f;
            }
        }
        else
        {
            animator.SetFloat(Speed, 0);
        }

        if (Input.GetButtonDown("Fire3"))
        {
            if (!isRun)
            {
                isRun = true;
                animator.SetBool(Run, true);
                Debug.Log("Режим бега включен");
            }
            else
            {
                isRun = false;
                animator.SetBool(Run, false);
                Debug.Log("Режим бега выключен");
            }
        }

     


        //Mathf.Abs - число по модулю
        if (Mathf.Abs(rig.velocity.x) < 0.01f)
        {
            return;
        }

        var angle = rig.velocity.x > 0f ? 0f : 180f;
        graphics.localEulerAngles = new Vector3(0f, angle, 0f);
    }

    public override void Move(Vector2 direction)
    {
        Vector2 velocity = rig.velocity;
        velocity.x = direction.x * maxSpeed;
        rig.velocity = velocity;
    }

    public override void Stop(float timer)
    {
    }

    public override void Jump(float force)
    {
        rig.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
    }

    private void OnJumpAction(float force)
    {
        if (IsGrounded() && !IsFreezing)
        {
            Jump(jumpForce * force);
            animator.SetTrigger(JumpPlayer);
        }
    }

    private bool IsGrounded()
    {
        Vector2 point = transform.position;
        point.y -= 0.1f; //чтобы избежать пересечения
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.down, 0.2f);
        return hit.collider != null;
    }
}